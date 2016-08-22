$(function () {

    var $serviceId = 1;
    $('#ServiceId').change(function () {
        $serviceId = $(this).val();
        //alert($serviceId);
    });

    $('#AddServiceToMaintenance').click(function () {
        var $a = $(this);
        var $maintenanceId = $a.attr("data-amt-id");
        //alert($serviceId);
        var $url = $a.attr("href");
        $.getJSON($url, { maintenanceId: $maintenanceId, serviceId: $serviceId })
    });


    $('#MakeId').change(function () {
        var $makeId = $(this).val();
        $.getJSON("../Cars/GetModelByMakeId/", { makeId: $makeId },
               function (data) {
                   var select = $("#ModelId");
                   select.empty();
                   select.append($('<option/>', {
                       value: 0,
                       text: "Select a Model"
                   }));
                   $.each(data, function (index, itemData) {
                       select.append($('<option/>', {
                           value: itemData.Value,
                           text: itemData.Text
                       }));
                   });
               });

    });

    var ajaxFormSubmit = function () {
        var $form = $(this);
        var options = {
            url: $form.attr("action"),
            type: $form.attr("method"),
            data: $form.serialize()
        };

        $.ajax(options).done(function (data) {
            var $target = $($form.attr("data-amt-target"));
            $target.replaceWith(data);
        });

        return false
    };

    var submitAutoCompleteForm = function(event, ui)
    {
        var $input = $(this);
        $input.val(ui.item.label);
        var $form = $input.parents("form:first");
        $form.submit();
    }

    var createAutoComplete = function ()
    {
        var $input = $(this);
        var options = {
            source: $input.attr("data-amt-autocomplete"),
            select: submitAutoCompleteForm
        };

        $input.autocomplete(options);
    }

    var getPage = function ()
    {
        var $a = $(this);
        var options = {
            url: $a.attr("href"),
            data: $("form").serialize(),
            type: "get"
        };

        $.ajax(options).done(function (data) {
            var target = $a.parents("div.pagedList").attr("data-amt-target");
            $(target).replaceWith(data);
        });

        return false
    }

    $("form[data-amt-ajax='true']").submit(ajaxFormSubmit);
    $("input[data-amt-autocomplete]").each(createAutoComplete)

    $(".pagedList").on("click", ".pagedList a", getPage);
    $(".body-content").on("click", ".pagedList a", getPage);
});