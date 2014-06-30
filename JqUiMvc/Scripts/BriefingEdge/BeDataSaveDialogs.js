
var dlgResponseCallback = function () { };

function beDlgSaveAndContinueErrors(callMe) {
    dlgResponseCallback = callMe;
    $("#beDlgSaveContinueErrors").dialog("open");
}
function beDlgNavStepErrors(callMe) {
    dlgResponseCallback = callMe;
    $("#beDlgNavStepErrors").dialog("open");
}
function beDlgNavStepChanges(callMe) {
    dlgResponseCallback = callMe;
    $("#beDlgNavStepChanges").dialog("open");
}
$(function () {
    $("#beDlgSaveContinueErrors").dialog({
        autoOpen: false,
        modal: true,
        resizable: false,
        buttons: {
            "Fix Errors": function () {
                $(this).dialog("close");
                dlgResponseCallback ( "stay");
            },
            "Discard Changes": function () {
                $(this).dialog("close");
                dlgResponseCallback ( "go");
            }
        }
    });
    $("#beDlgNavStepErrors").dialog({
        autoOpen: false,
        modal: true,
        resizable: false,
        buttons: {
            "Fix Errors": function () {
                $(this).dialog("close");
                dlgResponseCallback ( "stay");
            },
            "Discard Changes": function () {
                $(this).dialog("close");
                dlgResponseCallback ( "go");
            }
        }
    });
    $("#beDlgNavStepChanges").dialog({
        autoOpen: false,
        modal: true,
        resizable: false,
        buttons: {
            "Save Changes": function () {
                $(this).dialog("close");
                dlgResponseCallback ( "save");
            },
            "Stay on this Page": function () {
                $(this).dialog("close");
                dlgResponseCallback ( "stay");
            },
            "Discard Changes": function () {
                $(this).dialog("close");
                dlgResponseCallback ( "go");
            }
        }
    });
});
