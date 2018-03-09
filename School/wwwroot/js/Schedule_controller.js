$(function () {
    dragNdrop.init();
});


var dragNdrop = {
    init: function () {
        $("#teachers").sortable({
            items: "li",
            revert: false,
            opacity: 0.75 // прозрачность перемещяемого элемента
        });

        $("#schedule_table tbody tr td").droppable({
            accept: ".left.clearfix",
            classes: {
                //"ui-droppable-active": "ui-state-active",
                "ui-droppable-hover": "ui-state-hover"
            },
            drop: function (event, ui) {
                $("#error_message").hide();

                var obj = {
                    date: $('[id*=schedule_table] th').eq($(event.target).index()).attr("data-date"), // Дата
                    time_id: $(event.target).parent().find("th[data-lesson-id]").attr("data-lesson-id"),
                    teacher_id: ui.draggable.children().attr("data-teacher-id"),//учитель  
                    grade_id: $("#GradeID option:selected").attr("value") //класс 
                };

                var append_li_bind = dragNdrop.append_li.bind($(this), ui);

                server.save(obj).then(function (result) {
                    if (result.is_done == true) {
                        append_li_bind(result.schedule_id);
                    } else if (result.is_done == false) {
                        $("#error_message").show().text(result.message);
                    }
                    LoadingDialog.Stop();
                });

            }
        });
    },
    append_li: function (ui, schedule_id) {
        $(this).find("div.clearfix").remove();//удаляем учителя, если он уже есть в данном времени
        $(this).append(ui.draggable.html());
        $(this).find("small.glyphicon-remove").attr("data-schedule-id", schedule_id);//добавляем schedule_id для удаления в будущем
    }
}

var table = {
    remove_cell: function () {
        $(this).remove();
    }
};

var server = {
    save: function (obj) {
        LoadingDialog.Start();
        return $.ajax({
            type: 'POST',
            url: './Schedule/AddSchedule/',
            data: obj,
            success: function (result) { },
            error: function () {
                alert('Ошибка при сохранении');
                LoadingDialog.Stop();
                return false;
            }
        });
    },
    remove: function (context) {
        $("#error_message").hide();
        let id = $(context).attr("data-schedule-id");

        if (id) {

            LoadingDialog.Start();
            var remove_cell = table.remove_cell.bind($(context).closest("div.clearfix"));

            return $.ajax({
                type: 'GET',
                url: './Schedule/Delete/' + id,
                success: function (result) {
                    if (result == true) {
                        remove_cell();
                    }
                    $("#error_message").show("Не удалось удалить объект.");
                    LoadingDialog.Stop();
                },
                error: function () {
                    alert('Ошибка при сохранении');
                    LoadingDialog.Stop();
                    return false;
                }
            });
        }
    }
}
