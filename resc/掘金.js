// ==UserScript==
// @name         掘金系统检查
// @namespace    http://tampermonkey.net/
// @version      1.3 更新
// @description  检查掘金系统是否有未处理线索频率为10分钟
// @author       王艳朋
// @match        http://ztcrm.zotye.com/saleClue_queryPagedList.action*
// @grant        none
// @require    http://code.jquery.com/jquery-1.11.0.min.js
// ==/UserScript==

(function() {

    var audioElement = document.createElement('audio');
    audioElement.setAttribute('src', 'http://xmdx.sc.chinaz.com/Files/DownLoad/sound1/201701/8214.wav');
    //audioElement.setAttribute('autoplay', 'autoplay'); //打开自动播放
    //audioElement.load()
    $.get();
    audioElement.addEventListener("load", function(){
        //audioElement.play();
    }, true);

    function playSound(){
        audioElement.play();
    }


    //---------------------------------------------------------------//

    var keyword="未处理";//已
    var frequency=3;
    var UPDATE_HAVE = 5;
    var UPDATE_None = 15;
    var t;
    $('table.tbl_ tr').each(function(){
        var status = $(this).find("td").eq(7).text();

        if(status.indexOf(keyword)!=-1)
        {
            frequency=UPDATE_HAVE;//半分钟刷新一次
            playSound();
            return false;
        }
        else
        {
            frequency=UPDATE_None;//10分钟刷新一次
        }
    });
     patrn = /总共：<span id="pageCount">(\d+)<\/span> 页/g;
     patrn.exec($(document.body).html());
    var pageCount =parseInt(RegExp.$1);
    var patrn=/当前:第(\d+)页/g;
    patrn.exec($(document.body).html());
    var currentPage =parseInt(RegExp.$1);
    var p = pageCount;
    if(p>5) p = 5;
    
    if(currentPage!=="" && frequency!==UPDATE_HAVE && currentPage<p)
    {
        page2N=currentPage+1;
       
        pageTo('/saleClue_queryPagedList.action',page2N);
    }

    var msg = frequency==UPDATE_HAVE?"<h1 style='color:red;font-size:40px'>有线索未联系</h1>":"<h1 style='color:green;font-size:40px'>无未处理线索</h1>";
    var now = (new Date()).toLocaleString();
    var boarddiv = "<div style='font-size:20px; background:white;width:300px;height:200px;z-index:999;position:absolute;top:0;padding:10px'>"+msg+"<br/>最后检查时间："+now+"<br/>下次更新时间："+frequency+"分后</div>";
    $(document.body).append(boarddiv);
    t=setTimeout(function(){
        window.location.href  = "http://ztcrm.zotye.com/saleClue_queryPagedList.action" + "?r=" + Math.random();
        //location.reload();
    },frequency*60*1000);
})();