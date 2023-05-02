// Postback - when a drop down value of Record Per Page is changed
$("#RecordPerPage_Id").on('change', function () {
    $("#IsFieldOrderChanged").val(false);
    $("#form").submit();
});
