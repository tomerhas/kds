function Mid(str, start, len) {
    // Make sure start and len are within proper bounds
    if (start < 0 || len < 0) return "";
    var iEnd, iLen = String(str).length;
    if (start + len > iLen)
        iEnd = iLen;
    else
        iEnd = start + len;
    return String(str).substring(start, iEnd);
}
function padLeft(cID, ch, num) { 
    var elem = cID;
    var pad = elem; 
    if (!ch) ch = " ";

    while(String(pad).length < num) 
     pad = ch + pad;

    elem = pad;
    return elem;
}

if (typeof(Sys) !== 'undefined') Sys.Application.notifyScriptLoaded(); 
