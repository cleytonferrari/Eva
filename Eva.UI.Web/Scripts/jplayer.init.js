$(function(){
"use strict";
    $("#jquery_jplayer_1").jPlayer({
        ready: function () {
            $(this).jPlayer("setMedia", {
                rtmpv: "http://localhost:19914/Api/Midia/160b7e4448aa4a20894069d0eb97eb26",
                poster: "Content/img/space_alone.jpg"
            });
        },
        swfPath: "Content/img",
        supplied: "rtmpv",
        size: {
            width: "100%",
            height: "260px",
            cssClass: "jp-video-360p"
        },
        cssSelectorAncestor: "#jp_container_1",
        smoothPlayBar: true,
        keyEnabled: true
    });

    $("#jquery_jplayer_2").jPlayer({
        ready: function (event) {
            $(this).jPlayer("setMedia", {
                m4a:"http://www.jplayer.org/audio/m4a/TSP-01-Cro_magnon_man.m4a",
                oga:"http://www.jplayer.org/audio/ogg/TSP-01-Cro_magnon_man.ogg"
            });
        },
        swfPath: "Content/img",
        supplied: "m4a, oga",
        size: {

            cssClass: "jp-audio"
        },
        wmode: "window",
        cssSelectorAncestor: "#jp_container_2",
        smoothPlayBar: true,
        keyEnabled: true
    });
});