$(document).ready(function(){

 var btnMHidden = "作品要求";
 document.getElementById("btnMHidden").value = btnMHidden;

 /* 滑入按钮显示DIV，DIV ID divClose ，按钮ID btnMHidden*/
  $("#btnMHidden").mouseover
    (function(){
     document.getElementById("divClose").style.display="block";
      })
 /* 点击按钮隐藏DIV，按钮ID btnHClose */     
 $("#btnHClose").click
   (function(){
     document.getElementById("divClose").style.display="none";   
   })

 //$('.float-close').click(function () {
 //    ml_close_demo();
 //    return false;
 //});
 //$('.open-btn').click(function () {
 //    ml_open_demo();
 //    return false;
 //});

 //setTimeout(function () { ml_close_demo() }, 1000);
})
/*滑入弹出下拉*/
function onft1() {
    document.getElementById('HiddenDiv1').style.display = 'inline';

}
function onbt1() {
    document.getElementById('HiddenDiv1').style.display = 'none';

}
function onft2() {
    document.getElementById('HiddenDiv2').style.display = 'inline';

}
function onbt2() {
    document.getElementById('HiddenDiv2').style.display = 'none';

}
function onft3() {
    document.getElementById('HiddenDiv3').style.display = 'inline';

}
function onbt3() {
    document.getElementById('HiddenDiv3').style.display = 'none';

}
function onft4() {
    document.getElementById('HiddenDiv4').style.display = 'inline';

}
function onbt4() {
    document.getElementById('HiddenDiv4').style.display = 'none';

}
function onft5() {
    document.getElementById('HiddenDiv5').style.display = 'inline';

}
function onbt5() {
    document.getElementById('HiddenDiv5').style.display = 'none';

}
/* 关闭*/

window.onbeforeunload = function()
{    
    var n = window.event.screenX - window.screenLeft;    
    var b = n > document.documentElement.scrollWidth-20;    
    if(b && window.event.clientY < 0 || window.event.altKey)  // 是关闭而非刷新 
    {    
        document.getElementById('btnClose').click();
    }    
}  
document.onkeydown=function() 
{    
    if (event.keyCode==116)//屏蔽 F5 刷新键
    {    
        event.keyCode=0;    
        event.returnValue=false;    
    }    
}
// JScript 文件

function OpenDivWindow2(url, title, width, height, scrolling, top, left) {
    var divWindow = document.getElementById("divShowDivWindow");
    divWindow.style.top = top + "px";
    divWindow.style.left = left + "px";
    divWindow.style.visibility = "visible";
    divWindow.style.zIndex = 999;
    divWindow.style.width = (width + 0) + "px";
    divWindow.style.height = (height + 30) + "px";

    var divTitle = document.getElementById("divShowDivWindowTitle");
    divTitle.innerText = title;

    var divIframe = document.getElementById("divShowDivWindowIframe");
    divIframe.src = url;
    divIframe.width = width;
    divIframe.height = height;
    if (scrolling) {
        divIframe.scrolling = scrolling;
    }
}

function OpenDivWindow(url, title, width, height, top, left, scrolling) {
    var divWindow = document.getElementById("divShowDivWindow");

    if (!top) top = document.documentElement.scrollTop + document.body.scrollTop;
    top = top + 10;
    if (!left) left = document.body.clientWidth / 2 - divWindow.clientWidth / 2;
    if (!width) width == document.documentElement.clientWidth - 20;
    if (!height) height = document.documentElement.clientHeight - 50;
    var scrolling = "on";
    OpenDivWindow2(url, title, width, height, scrolling, top, left);
}


function CloseDivWindow() {
    var divWindow = document.getElementById("divShowDivWindow");
    divWindow.style.top = "-9999px";
    divWindow.style.visibility = "hidden";

}