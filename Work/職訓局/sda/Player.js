var audio = document.getElementById("audio");
var play = document.getElementById("play");
//var pause = document.getElementById("pause");
var stop = document.getElementById("stop");
var info = document.getElementById("info");
var music = document.getElementById("music");
var song = document.getElementById("song");
var prevtime = document.getElementById("prevtime");
var nexttime = document.getElementById("nexttime");
var prevsong = document.getElementById("prevsong");
var nextsong = document.getElementById("nextsong");

var vol = document.getElementById("vol");
var volM = document.getElementById("volM");
var volP = document.getElementById("volP");
var volValue = document.getElementById("volValue");
var muted = document.getElementById("muted");
var loop = document.getElementById("loop");
var random = document.getElementById("random");
var allloop = document.getElementById("allloop");

var info2 = document.getElementById("info2");
var duration = document.getElementById("duration");
var settime = document.getElementById("settime");

var Sbook = document.getElementById("Sbook");
var Tbook = document.getElementById("Tbook");
var btnUpdateMusic = document.getElementById("btnUpdateMusic");
var songbook = document.getElementById("songbook");
var book = document.getElementById("book");


play.addEventListener("click", PlayMusic);
//pause.addEventListener("click", PauseMusic);
stop.addEventListener("click", StopMusic);
music.addEventListener("change", function () { ChangeMusic(music.selectedIndex); });
prevtime.addEventListener("click", function () { TimeChange(false); });
nexttime.addEventListener("click", function () { TimeChange(true); });
prevsong.addEventListener("click", function () { SongChange("p"); });
nextsong.addEventListener("click", function () { SongChange("n"); });
vol.addEventListener(BrowserTest(), function () { VolumeChange("c"); });
volM.addEventListener("click", function () { VolumeChange("m"); });
volP.addEventListener("click", function () { VolumeChange("p"); });
muted.addEventListener("click", SetMuted);
loop.addEventListener("click", loopSong);
random.addEventListener("click", randomSong);
allloop.addEventListener("click", allloopSong);
settime.addEventListener("click", function () { setTimeUseBar(event); });

Sbook.addEventListener("dragstart", function () { drag(event); });
Tbook.addEventListener("dragover", function () { allowDrop(event); });
Tbook.addEventListener("drop", function () { drop(event); });

Tbook.addEventListener("dragstart", function () { drag(event); });
Sbook.addEventListener("dragover", function () { allowDrop(event); });
Sbook.addEventListener("drop", function () { drop(event); });

btnUpdateMusic.addEventListener("click", UpdateMusic);
songbook.addEventListener("click", function () { displayBook(book.title) });

VolumeChange("c");  //設定初始音量
getDefaultSong();   //設定初始歌單

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

function displayBook(displayFlag) {
    if (displayFlag == "hide") {
        book.title = "show";

    }
    else {
        book.title = "hide";

    }
    book.className = book.title;
}

//更新歌單
function UpdateMusic() {
    //清掉所有歌
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
    evt.target.appendChild(document.getElementById(data));
}

function allowDrop(evt) {
    evt.preventDefault();
}


function drag(evt) {
    evt.dataTransfer.setData("text", evt.target.id);
    //alert(evt.target.id);
}


//抓預設歌曲
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

function setTimeUseBar(evt) {
    //console.log(ControlPanel.style.width);
    audio.currentTime = audio.duration * (evt.offsetX / parseInt(ControlPanel.style.width));
    //console.log(ControlPanel.style.width);
}


function getDuration() {
    var durationTime = getTimeFormat(audio.duration);
    var currentTime = getTimeFormat(audio.currentTime);
    duration.innerText = currentTime + " / " + durationTime;

    progress.style.width = Math.floor(audio.currentTime / audio.duration * 100) + "%";
    console.log(progress.style.width);
    setTimeout(getDuration, 1000);

    if (durationTime == currentTime) {
        if (info2.innerText == "單曲循環") {
            SongChange("s");
        }
        else if (info2.innerText == "隨機播放") {
            SongChange("r");
        }
        else if (music.selectedIndex == music.options.length - 1) {
            if (info2.innerText == "全曲循環") {
                SongChange("a");

            }
            else {
                StopMusic();
            }
        }
        else {
            SongChange("c");

        }

    }

}


function getTimeFormat(TimeSec) {
    var min = Math.floor(TimeSec / 60);
    var sec = Math.floor(TimeSec) - min * 60;
    min = min < 10 ? "0" + min : min;
    sec = sec < 10 ? "0" + sec : sec;
    return min + ":" + sec;
}

//全曲循環
function allloopSong() {
    if (info2.innerText == "全曲循環") {
        info2.innerText = "";
    }
    else {
        info2.innerText = "全曲循環";
    }
}

//隨機播放
function randomSong() {
    if (info2.innerText == "隨機播放") {
        info2.innerText = "";
    }
    else {
        info2.innerText = "隨機播放";
    }
}

//單曲循環
function loopSong() {
    if (info2.innerText == "單曲循環") {
        info2.innerText = "";
    }
    else {
        info2.innerText = "單曲循環";
    }
}



function SetMuted() {
    if (muted.innerText == "V") {
        audio.muted = true;
        muted.innerText = "U";
    }
    else {
        audio.muted = false;
        muted.innerText = "V";
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
    volValue.value = vol.value;

    audio.volume = volValue.value / 100;
}

function SongChange(strFlag) {
    var index = music.selectedIndex;
    var length = music.options.length;

    switch (strFlag) {
        case "n":
            if (index < length - 1)
                index++;
            else
                index = 0;
            break;
        case "p":
            if (index == 0)
                index = length - 1;
            else
                index--;
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

    //song.src = music.options[index].value;
    //music.options[index].selected = true;

    ChangeMusic(index);
}

function TimeChange(boolFlag) {
    if (boolFlag)
        audio.currentTime += 10;
    else
        audio.currentTime -= 10;
}


function PlayMusic() {
    audio.play();
    getDuration();
    ShowStatus(1);
    if (play.innerText == "4")
        play.innerText = ";";
    else {
        play.innerText = "4";
        PauseMusic();
    }
}
function PauseMusic() {
    audio.pause();
    ShowStatus(2);
}


function StopMusic() {
    audio.pause();
    audio.load();
    play.innerText = "4";
    ShowStatus(3);

}


function ChangeMusic(index) {
    song.src = music.options[index].value;
    song.title = music.options[index].innerText;
    console.log(song.title);
    music.options[index].selected = true;
    audio.load();

    if (play.innerText == ";") {
        audio.play();
        ShowStatus(1);
    }
    //PlayMusic();
}

function ShowStatus(status) {
    var strStatus = "";

    switch (status) {
        case 1:
            strStatus = "現正播放：" + song.title;
            break;
        case 2:
            strStatus = "音樂暫停";
            break;
        case 3:
            strStatus = "音樂停止";
            break;
    }

    info.innerText = strStatus;
}