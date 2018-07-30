$(document).ready(function () {
    // Handlers
    $(".label").on("click", editBtnClicked);
    $("#submit").on("click", submitBtnClicked);
    // Edit Button
    function editBtnClicked(evt) {
        evt.preventDefault();
        var closestRow = $(this).closest("tr");
        clickedId = closestRow.find("td.tagId").text();
        console.log(clickedId);
        $.ajax({
            url: '/api/meta/' + clickedId,
            method: 'GET',
            data: {},
            success: function (data) {
                console.log(data);
                $("#metaName").text(data.item.metaName);
                $("#metaDescription").text(data.item.metaDescription);
                $("#value").val(data.item.value);
            },
            error: function (error) {
                alert("Error retrieving data.");
                console.log("Get Failed", error);
                _errorParse(error);
            }
        });
    }
    // Submit Button
    function submitBtnClicked(evt) {
        evt.preventDefault();
        tagValue = $("#value").val();
        console.log(clickedId);
        console.log(tagValue);
        $.ajax({
            url: '/api/meta/value/' + clickedId,
            method: 'PUT',
            data: {
                "id": clickedId,
                "value": tagValue
            },
            success: function (data) {
                console.log(data);
                window.location.reload();
            },
            error: function (error) {
                alert("Error updating data.");
                console.log("Put Failed", error);
                _errorParse(error);
            }
        });
    }
    // Foo Table Hydrate
    var id = parseInt($("#modelId").val());
    $('.footable').footable();
    function createRow(item) {
        var row = $('<tr><td>' + item.metaName + '</td><td class="tagId">' + item.id + '</td><td>' + item.value + '</td><td>' + item.metaDescription +
            '</td><td><a data-toggle="modal" class="label label-warning" href="#modal-form"><i class="fa fa-edit"></i></a></td></tr>');
        return row;
    }
    $.ajax({
        url: '/api/meta/url/' + id,
        data: {},
        success: function (data) {
            $.each(data.items, function (index, item) {
                var row = createRow(item);
                row.find(".label").on("click", editBtnClicked);
                $('table tbody').append(row);
            });
            $('.footable').trigger('footable_initialize');
        },
        error: function (error) {
            alert("Error retrieving data.");
            console.log("Get Failed", error);
            _errorParse(error);
        }
    });
    // Called when error is thrown
    function _errorParse(data) {

        var newData = {};

        // Change "modifiedBy" to logged in user
        // Filter "errorSeverity" 
        // Try to find way to log error line

        // console.trace(); for debugging stack

        // If error is critical send email

        newData = {
            "errorMessage": data.responseJSON.message,
            "errorNumber": data.status,
            "modifiedBy": "Admin",
            "errorSeverity": 0,
            "errorState": 0,
            "errorProcedure": "Method",
            "errorLine": 0
        };

        // Sends error data to DB
        console.log(data);
        _postError(newData);
    }
    // Function to catch errors
    function _postError(data) {
        console.log(data);
        $.ajax({
            url: '/api/errors/',
            method: 'POST',
            data: data,
            success: function (res) {
                console.log("Error successfully logged", res);
            },
            error: function (err) {
                console.log("Error not logged", err);
            }
        });
    }
});