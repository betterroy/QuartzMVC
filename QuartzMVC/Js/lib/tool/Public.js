/// <reference path="../layer/layer.js" />

//获取地址栏信息
function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}

//自定义layer弹出
function showUrl(title, url, wight, height) {
    var dwight = "800px";
    var dheight = "500px";
    if (height) {
        dheight = height + "px";
    }
    if (wight) {
        dwight = wight + "px";
    }
    layer.open({
        type: 2,
        area: [dwight, dheight],
        title: title,
        fixed: false, //不固定
        maxmin: true,
        content: url
    });
}
// 不在这个页面引用layer.js
//function showUrl(title, url, wight, height) {
//    var dwight = "800px";
//    var dheight = "500px";
//    if (height) {
//        dheight = height + "px";
//    }
//    if (wight) {
//        dwight = wight + "px";
//    }

//    //关键
//    var myLayer = window.parent.layer;
//    if (!myLayer) {
//        myLayer = layer;
//    }
//    myLayer.open({
//        type: 2,
//        area: [dwight, dheight],
//        title: title,
//        content: url
//    });
//}


//刷新方法 调用方法 setTimeout('myrefresh()', 1000); //隔一秒刷新
function myrefresh() {
    window.location.reload();
}
