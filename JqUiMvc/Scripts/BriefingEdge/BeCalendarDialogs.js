//var callbackWithDates = function (startDate,endDate,roomID) { };

function ShowCalendarDialog(urlAction, callbackWithDates) {
    //var $loading = $('<img src="../../Content/images/ajaxLoading.gif" alt="loading" class="ui-loading-icon">');

    $("<div></div>")
    .addClass("dialog")
    .attr("id", $(this)
    .attr("data-dialog-id"))
    .appendTo("body")
    .append('<img src="../../../images/ArrowIcons/icon_Calendar.png" alt="loading" class="Xui-loading-icon">')
    .dialog({
        title: "Select the Visit Date",
        modal: true,
        height: 500,
        width: 600,
        buttons: {
            "OK": function () {
                var startDate = new Date($("#storeStart").val());
                var endDate = new Date($("#storeEnd").val());
                var roomID = 2;
                var yep = GetModel();
                $(this).dialog("close");
                callbackWithDates(startDate, endDate, roomID);
            },
            Cancel: function () {
                $(this).dialog("close");
            }
        },
        close: function () { $(this).remove(); }, // Important! Allows dialog to be reloaded after initial load

    })
    .load(urlAction); // Here's where it calls the given Controller Action to get the actual dialog innards.
}
function GetModel()
{
    var model = {
        "VisitID" : $("#VisitID").val(),
        "SiteID" : $("#SiteID").val(),
        "VisitTypeID" : $("#VisitTypeID").val(),
        "StartDate" : $("#storeStart").val(),
        "EndDate" : $("#storeEnd").val()
    };

    return model;
}