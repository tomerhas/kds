var Prefix;
var iCounterPrefix = 0;
var ObjTextBoxId = "", ObjValidTextBoxId = "", ObjListItemId = "", ObjButtonId = "", ObjListOfValuesId = "";
var ObjTextBox, ObjListItem, ObjButton, ObjListOfValues;
function PutValueInListItem(ButtonName, ObjTextBoxName, ObjListItemName, ObjListOfValues) {
    ObjTextBoxId = SetNameOfObject(ButtonName, ObjTextBoxName);
    ObjListItemId = SetNameOfObject(ButtonName, ObjListItemName);
    ObjListOfValuesId = SetNameOfObject(ButtonName, ObjListOfValues);
    ObjTextBox = document.getElementById(ObjTextBoxId);
    ObjListItem = document.getElementById(ObjListItemId);
    ObjListOfValues = document.getElementById(ObjListOfValuesId);
    if (IsElementExist(ObjListItem, ObjTextBox.value) == true) {
        alert('הערך כבר נבחר');
    }
    else {
        var newOption = document.createElement("option");
        newOption.value = ObjTextBox.value;
        newOption.text = ObjTextBox.value;
        ObjListItem.add(newOption);
    }
    ObjTextBox.value = "";
    ObjButton.disabled = (ObjTextBox.value == "") ? true : false;
    ObjListOfValues.value = GetSelectedItems(ObjListItem);
}
function SetNameOfObject(ObjectInServer,ObjNameInServer) {
    Prefix = ObjectInServer.id.split("_");
    var CurrentObj= "";
    for (iCounterPrefix = 0; iCounterPrefix < Prefix.length - 1; iCounterPrefix++) {
        CurrentObj = CurrentObj + Prefix[iCounterPrefix] + "_";
    }
    return CurrentObj + ObjNameInServer;
}

function EnableButtonAdd(source, eventArgs) {
    ObjTextBoxId = source.get_element().id;
    ObjTextBox = document.getElementById(ObjTextBoxId);
    ObjButtonId = ObjTextBox.attributes["BtnAdd"].value;// ObjTextBox.BtnAdd;
    ObjValidTextBoxId = ObjTextBox.attributes["ValidText"].value; //ObjTextBox.ValidText;
    ObjValidTextBox = document.getElementById(ObjValidTextBoxId);
    ObjValidTextBox.value = eventArgs.get_text();
    ObjButton = document.getElementById(ObjButtonId);
    ObjButton.disabled = false;
}
function SetButtonAdd(ObjTextBoxId) {
    ObjTextBox = document.getElementById(ObjTextBoxId);
    ObjButtonId = ObjTextBox.attributes["BtnAdd"].value;  //ObjTextBox.BtnAdd;
    ObjButton = document.getElementById(ObjButtonId);
    ObjButton.disabled = (ObjTextBox.value == "") ;
}

function ClearValidText(ObjTextBoxId) {
   // debugger;
    ObjTextBox = document.getElementById(ObjTextBoxId);
    ObjValidTextBoxId = ObjTextBox.attributes["ValidText"].value; //ObjTextBox.ValidText;
    ObjButtonId = ObjTextBox.attributes["BtnAdd"].value;// ObjTextBox.BtnAdd;
    ObjTextBox = document.getElementById(ObjValidTextBoxId);
    ObjButton = document.getElementById(ObjButtonId);
    ObjButton.disabled = true;
    ObjTextBox.value = ""; 
}

function EnableButtonDel(ObjectInServer, ObjNameInServer) {
    ObjButtonId = SetNameOfObject(ObjectInServer, ObjNameInServer);
    ObjButton = document.getElementById(ObjButtonId);
    ObjListItem = document.getElementById(ObjectInServer.id);
    ObjButton.disabled = (AreSelectedItems(ObjListItem)) ? false : true;
}

function AreSelectedItems(ObjListItem) {
    var src = document.getElementById(ObjListItem.id);
    for (var count = 0; count < src.options.length; count++) {
        if (src.options[count].selected == true) {
            return true;
        }
    }
    return false;
}
function IsElementExist(ObjListItem, Element) {
    var src = document.getElementById(ObjListItem.id);
    for (var count = 0; count < src.options.length; count++) {
        if (src.options[count].text == Element) {
            return true;
        }
    }
    return false;
}

function GetSelectedItems(ObjListItem) {
    var src = document.getElementById(ObjListItem.id);
    var List = ""; 
    for (var count = 0; count < src.options.length; count++) {
            List = List + src.options[count].text + ",";
    }
    return List.substr(0, (List.length) - 1);
}

function DeleteSelectedItems(ObjectInServer, ObjNameInServer, ObjListOfValues) {
    ObjListItemId = SetNameOfObject(ObjectInServer, ObjNameInServer);
    ObjListOfValuesId = SetNameOfObject(ObjectInServer, ObjListOfValues);
    ObjListOfValues = document.getElementById(ObjListOfValuesId);

 
    ObjButton = document.getElementById(ObjectInServer.id);
    var src = document.getElementById(ObjListItem.id);
    for (var count = 0; count < src.options.length; count++) {
        if (src.options[count].selected == true) {
            src.remove(count);
            count--;
        }
    }
    ObjListOfValues.value = GetSelectedItems(ObjListItem);
    ObjButton.disabled = (AreSelectedItems(ObjListItem)) ? false : true;
}
if (typeof(Sys) !== 'undefined') Sys.Application.notifyScriptLoaded(); 

