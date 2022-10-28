var ControlPanel = document.getElementById("ControlPanel");
var play = document.getElementById("play");
var stop = document.getElementById("stop");
var audio = document.getElementById("audio");
var info = document.getElementById("info");
var info2 = document.getElementById("info2");
var song = document.getElementById("song");
var prevtime = document.getElementById("prevtime");
var nexttime = document.getElementById("nexttime");
var prevsong = document.getElementById("prevsong");
var nextsong = document.getElementById("nextsong");
var music = document.getElementById("music");
var vol = document.getElementById("vol");
var volM = document.getElementById("volM");
var volP = document.getElementById("volP");
var volValue = document.getElementById("volValue");
var muted = document.getElementById("muted");
var duration = document.getElementById("duration");
var settime = document.getElementById("settime");
var progress = document.getElementById("progress");
var loop = document.getElementById("loop");
var random = document.getElementById("random");
var allloop = document.getElementById("allloop");
var Sbook = document.getElementById("Sbook");
var Tbook = document.getElementById("Tbook");
var book = document.getElementById("book");
var btnUpdateMusic = document.getElementById("btnUpdateMusic");
var songbook = document.getElementById("songbook");

play.addEventListener("click", PlayMusic);
stop.addEventListener("click", StopMusic);
prevtime.addEventListener("click", function () { TimeChange("p") });
nexttime.addEventListener("click", function () { TimeChange("n") });
prevsong.addEventListener("click", function () { SongChange("p") });
nextsong.addEventListener("click", function () { SongChange("n") });
volM.addEventListener("click", function () { VolumeChange("m") });
volP.addEventListener("click", function () { VolumeChange("p") });
vol.addEventListener(BrowserTest(), function () { VolumeChange("c") });
muted.addEventListener("click", SetMuted);
settime.addEventListener("click", function () { setTimeUseBar(event) });
loop.addEventListener("click", loopSong);
random.addEventListener("click", randomSong);
allloop.addEventListener("click", allloopSong);

songbook.addEventListener("click", function () { diplayBook(book.className) });

music.addEventListener("change", function () { MusicChange(music.selectedIndex) });


Sbook.addEventListener("dragstart", function () { drag(event) });
Tbook.addEventListener("dragover", function () { allowDrop(event) });
Tbook.addEventListener("drop", function () { drop(event) });

Tbook.addEventListener("dragstart", function () { drag(event) });
Sbook.addEventListener("dragover", function () { allowDrop(event) });
Sbook.addEventListener("drop", function () { drop(event) });

btnUpdateMusic.addEventListener("click", UpdateMusic);

VolumeChange("c"); //設定初始音量
getDefaultSong(); //設定初始歌曲

//瀏覽器偵測
function BrowserTest() {
    if (navigator.userAgent.search("Chrome") != -1)
        return "input";
    else if (navigator.userAgent.search("Opera") != -1)
        return "input";
    else if (navigator.userAgent.search("Firefox") != -1)
        return "input";
    else
        return "change";

}

function diplayBook(flag) {
    if (flag == "hide")
        book.className = "show";
    else {
        book.className = "hide";
    }


}

function UpdateMusic() {
    for (i = music.children.length - 1; i >= 0; i--) {
        music.removeChild(music.children[i]);
    }

    for (i = 0; i < Tbook.children.length; i++) {
        var option = document.createElement("option");
        var Snode = Tbook.children[i];
        option.value = Snode.title;
        option.innerText = Snode.innerText;
        music.appendChild(option);
    }
    SongChange("a");
}

function drop(evt) {
    evt.preventDefault();
    var data = evt.dataTransfer.getData("text");
    console.log(data);
    evt.target.appendChild(document.getElementById(data));
}

function allowDrop(evt) {
    evt.preventDefault();
}

function drag(evt) {
    evt.dataTransfer.setData("text", evt.target.id);
    //alert(evt.target.id);
}

function getDefaultSong() {
    for (i = 0; i < Sbook.children.length; i++) {
        var option = document.createElement("option");
        var Snode = Sbook.children[i];
        option.value = Snode.title;
        option.innerText = Snode.innerText;
        music.appendChild(option);

        Snode.draggable = true;
        Snode.id = "song" + (i + 1);
    }

    SongChange("a");
}

function randomSong() {
    if (info2.innerText == "隨機播放") {
        info2.innerText = "";
    }
    else
        info2.innerText = "隨機播放";
}

function allloopSong() {
    if (info2.innerText == "全曲循環") {
        info2.innerText = "";
    }
    else
        info2.innerText = "全曲循環";
}

function loopSong() {
    if (info2.innerText == "單曲循環") {
        info2.innerText = "";
    }
    else
        info2.innerText = "單曲循環";
}


function setTimeUseBar(evt) {
    //console.log(evt.target.id);
    audio.currentTime = (evt.offsetX / parseInt(ControlPanel.style.width)) * audio.duration;
}


function getDuration() {
    var durationTime = getTimeFormat(audio.duration);
    var currentTime = getTimeFormat(audio.currentTime);
    duration.innerText = currentTime + " / " + durationTime;


    //progress.style.width = Math.floor(audio.currentTime / audio.duration * 100) + "%";
    progress.style.width = audio.currentTime / audio.duration * 100 + "%";
    console.log(progress.style.width);
    setTimeout(getDuration, 10);

    if (durationTime == currentTime) {
        if (info2.innerText == "單曲循環") {
            SongChange("s");
        }
        else if (music.selectedIndex == music.options.length - 1) {
            if (info2.innerText == "全曲循環") {
                SongChange("a");
            }
            else {
                StopMusic();
            }
        }
        else if (info2.innerText == "隨機播放") {
            SongChange("r");
        }
        else {
            SongChange("c");
        }


        //SongChange("n");
    }

}
function getTimeFormat(TimeSec) {
    var min = Math.floor(TimeSec / 60);
    var sec = Math.floor(TimeSec - min * 60);
    min = min < 10 ? "0" + min : min;
    sec = sec < 10 ? "0" + sec : sec;
    return min + ":" + sec;
}

function MusicChange(index) {
    song.src = music.options[index].value;
    music.options[index].selected = true;
    song.title = music.options[index].innerText;
    audio.load();

    if (play.innerText == ";") {
        audio.play();
        ShowStatus(1);
    }

}


function SongChange(flag) {
    var index = music.selectedIndex;
    var length = music.options.length;  //抓歌曲數

    switch (flag) {
        case "p":
            if (index == 0)
                index = length - 1;
            else
                index--;
            break;
        case "n":
            if (index == length - 1)
                index = 0;
            else
                index++;
            break;
        case "s":
            audio.currentTime = 0;
            break;
        case "r":
            index = Math.floor(Math.random() * length);
            break;
        case "a":
            index = 0;
            break;
        default:
            index++;
            break;
    }
    console.log(index);

    MusicChange(index);
}


function SetMuted() {
    if (muted.innerText == "V") {
        audio.muted = true;
        muted.innerText = "U"
    }
    else {
        audio.muted = false;
        muted.innerText = "V"
    }

}



function VolumeChange(sound) {
    switch (sound) {
        case "m":
            vol.value--;

            break;
        case "p":
            vol.value++;
            break;
    }
    //vol.value = volValue.value;
    volValue.value = vol.value;
    audio.volume = volValue.value / 100;
    console.log(volValue.value / 100);

}


function TimeChange(flag) {
    if (flag == "p")
        audio.currentTime -= 10;
    else
        audio.currentTime += 10;
}

function StopMusic() {
    audio.pause();
    audio.load();
    play.innerText = "4";
    ShowStatus(3);
}

function PlayMusic() {
    if (play.innerText == ";") {
        audio.pause();
        play.innerText = "4"
        ShowStatus(2);
    }
    else {
        audio.play();
        getDuration();
        play.innerText = ";"
        ShowStatus(1);

    }
}

function ShowStatus(status) {
    var strStatus = "";

    switch (status) {
        case 1:
            strStatus = "現正播放曲目為「" + song.title + "」";
            break;
        case 2:
            strStatus = "音樂暫停中!!(現正播放曲目為「" + song.title + "」)";
            break;
        case 3:
            strStatus = "音樂停止中!!";;
            break;

    }

    info.innerText = strStatus;
}