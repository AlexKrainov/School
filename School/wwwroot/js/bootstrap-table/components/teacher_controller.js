; $(function () {
    table.init_table();
});

var table = {
    $table: null,
    init_table: function () {
        LoadingDialog.Start();

        table.reset_table();
        $("#teacher_container").load("../Teacher/ListTeachers", table.draw_table);

    },
    draw_table: function () {
        //http://bootstrap-table.wenzhixin.net.cn/documentation/#table-options
        table.$table = $('#teacher_table').bootstrapTable(
            {
                toolbar: "#toolbar",
                search: true,
                pagination: true,
                showToggle: true,
                stickyHeader: true,
                contextMenu: '#context-menu',
                onContextMenuItem: table.onContexRowtSelected
            }
        );

        //добавляем события
        table.$table.on('dbl-click-row.bs.table', table.dblclick);
        table.$table.on('click-row.bs.table', table.select_row);

        LoadingDialog.Stop();
    },
    select_row: function (e, row, $element) {
        $('.success').removeClass('success');
        $($element).addClass('success');
    },
    dblclick: function (e, row, $element) {
        LoadingDialog.Start();
        var id = $element.find("td:first").attr("data-id");
        dialog.edit_teacher(id);
    },
    onContexRowtSelected: function (row, $el) {
        LoadingDialog.Start();
        var id = $('.success').find("td:first").attr("data-id");

        switch ($($el).attr("data-item")) {
            case "create":
                dialog.create_teacher();
                break;
            case "edit":
                dialog.edit_teacher(id);
                break;
            case "delete":
                server.remove_teacher(id);
                break;
            default:
        }
    },
    //удаляем все строчки в таблице кроме хэдэра
    reset_table: function () {
        if (table.$table != null) {
            $("#table_teacher_container div").remove();
        }
    }
};

var dialog = {
    create_teacher: function () {
        LoadingDialog.Start();
        $("#dialog_content").load("../Teacher/Create/", dialog.open);
    },
    edit_teacher: function (id) {
        if (id != undefined) {
            LoadingDialog.Start();
            $("#dialog_content").load("../Teacher/Edit/" + id, dialog.open);
        }
    },
    open: function (e, row, $element) {
        $("#dialog").modal("show");
        LoadingDialog.Stop();
    },
    close: function (result) {
        dialog.reset();
        $("#dialog").modal("hide");
    },
    save: function (action) {
        LoadingDialog.Start();

        let data = $("#form_teacher").serialize();

        server.save_teacher(data, action)
            .then(function (result) {
                if (result == true) {
                    dialog.close();
                    table.init_table();
                } else {
                    alert("Не удалось сохранить значение, попробуйте снова.");
                }

                LoadingDialog.Stop();
            });
    },
    reset: function () {
        $("#dialog_content > div").remove();
    }
}

var tooltip = {
    init: function () {
        $('#create_class').popover(
            {
                content: '<div class="form-inline editableform"> <div class="control-group form-group"> <div> <div class="editable-input" style="position: relative;"> <input id="tooltip_text" type="text" class="form-control input-sm" style="width: 150px;"> </div> <div class="editable-buttons"> <button id="ok_popover" type="button" class="btn btn-primary btn-sm editable-submit" onclick="tooltip.ok()"> <i class="glyphicon glyphicon-ok"></i> </button> <button id="close_popover" type="button" class="btn btn-default btn-sm editable-cancel" onclick="tooltip.close()"><i class="glyphicon glyphicon-remove"></i></button> </div> </div> <small class=" text-muted">Введите название предмета</small> <div class="editable-error-block help-block ui-state-error" style="display: none; color: red;"></div> </div> </div>',
                placement: 'right',
                html: true,
            });
    },
    //Закрытие tooltip-a
    close: function () {
        tooltip.reset();
        $('#create_class').popover("hide");
    },
    //Сохранение
    ok: function () {
        //ToDo: Validation 
        var newClass = $("#tooltip_text").val();

        server.create_class(newClass).then(function (data) {

            if (data.iserror == false) {
                $("#SubjectsIDs").append("<option value='" + data.subject.id + "'>" + data.subject.name + "</option>")

                tooltip.close();
            } else if (data.iserror == true) {
                tooltip.error(data.message);
            }
        });
    },
    reset: function () {
        tooltip.error();
        $("#tooltip_text").val("");
    },
    error: function (message) {
        if (message) {
            $(".editable-error-block.ui-state-error").show().text(message);
        } else {
            $(".editable-error-block.ui-state-error").hide().text("");
        }

    }
};

var server = {
    save_teacher: function (json, action) {
        return $.ajax({
            type: 'POST',
            url: './Teacher/' + action,
            data: json,
            success: function (result) {
            },
            error: function () {
                alert('Ошибка при сохранении');
                LoadingDialog.Stop();
                return false;
            }
        });
    },
    remove_teacher: function (id) {
        if (id) {
            LoadingDialog.Start();
            return $.ajax({
                type: 'GET',
                url: './Teacher/Delete/' + id,
                success: function (result) {
                    if (result == true) {
                        table.init_table();
                    } else {
                        alert("Не удалось сохранить значение. Не все обязательные поля заполнены.");
                    }
                    LoadingDialog.Stop();
                },
                error: function () {
                    alert('Ошибка при удалении');
                    return false;
                }
            });
        }
    },
    create_class: function (grade) {
        //LoadingDialog.Start();
        return $.ajax({
            type: 'GET',
            url: './Teacher/CreateSubject/' + grade,
            //data: grade,
            success: function (result) { },
            error: function () {
                alert('Ошибка при сохранении');
                return false;
            }
        });
    }
}


function onReadyTable() {
}