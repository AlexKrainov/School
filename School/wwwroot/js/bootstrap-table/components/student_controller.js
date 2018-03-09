; $(function () {
    table.init_table();
});

var table = {
    $table: null,
    init_table: function () {
        LoadingDialog.Start();

        table.reset_table();
        $("#student_container").load("../Student/ListStudents", table.draw_table);

    },
    draw_table: function () {
        //http://bootstrap-table.wenzhixin.net.cn/documentation/#table-options
        table.$table = $('#student_table').bootstrapTable(
            {
                toolbar: "#toolbar",
                search: true,
                pagination: true,
                showToggle: true,
                //resizable: true,
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
        dialog.edit_student(id);
    },
    onContexRowtSelected: function (row, $el) {
        LoadingDialog.Start();
        var id = $('.success').find("td:first").attr("data-id");

        switch ($($el).attr("data-item")) {
            case "create":
                dialog.create_student();
                break;
            case "edit":
                dialog.edit_student(id);
                break;
            case "delete":
                server.remove_student(id);
                break;
            default:
        }
    },
    //удаляем все строчки в таблице кроме хэдэра
    reset_table: function () {
        if (table.$table != null) {
            $("#table_student_container div").remove();
        }
    }
};

var dialog = {
    create_student: function () {
        LoadingDialog.Start();
        $("#dialog_content").load("../Student/Create/", dialog.open);
    },
    edit_student: function (id) {
        if (id != undefined) {
            LoadingDialog.Start();
            $("#dialog_content").load("../Student/Edit/" + id, dialog.open);
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

        let data = $("#form_student").serialize();

        server.save_student(data, action)
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
                content: '<div class="form-inline editableform"> <div class="control-group form-group"> <div> <div class="editable-input" style="position: relative;"> <input id="tooltip_text" type="text" class="form-control input-sm" maxlength="2" style="width: 150px;"> </div> <div class="editable-buttons"> <button id="ok_popover" type="button" class="btn btn-primary btn-sm editable-submit" onclick="tooltip.ok()"> <i class="glyphicon glyphicon-ok"></i> </button> <button id="close_popover" type="button" class="btn btn-default btn-sm editable-cancel" onclick="tooltip.close()"><i class="glyphicon glyphicon-remove"></i></button> </div> </div> <small class=" text-muted">Введите название например, 5А</small> <div class="editable-error-block help-block ui-state-error" style="display: none; color: red;"></div> </div> </div>',
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
                $("#grades").append("<option value='" + data.grade.id + "'>" + data.grade.number + "'" + data.grade.symbol + "'</option>")

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
    save_student: function (json, action) {
        return $.ajax({
            type: 'POST',
            url: './Student/' + action,
            data: json,
            success: function (result) {
            },
            error: function () {
                alert('Ошибка при сохранении');
                return false;
            }
        });
    },
    remove_student: function (id) {
        if (id) {
            LoadingDialog.Start();
            return $.ajax({
                type: 'GET',
                url: './Student/Delete/' + id,
                success: function (result) {
                    if (result == true) {
                        table.init_table();
                    } else {
                        alert("Не удалось сохранить значение");
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
            url: './Student/CreateGrade/' + grade,
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