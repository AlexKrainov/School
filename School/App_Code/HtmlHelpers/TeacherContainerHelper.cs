using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using School.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace School.App_Code.HtmlHelpers
{
    public static class TeacherContainerHelper
    {
        /// <summary>
        /// Выдает блок с учителем в вида:
        /// <div class="chat-body clearfix" data-teacher-id="@teachers[i].ID">
        ///                    <div class="header">
        ///                        <strong class="primary-font">@teachers[i].LastName @teachers[i].FirstName @teachers[i].MiddleName</strong>
        ///                        <small class="pull-right text-muted glyphicon glyphicon-remove"></small>
        ///                    </div>
        ///                    <p>subjects</p>
        ///                </div>
        /// </summary>
        /// <param name="html"></param>
        /// <param name="teacher"></param>
        /// <returns></returns>
        public static HtmlString CreateTeacherContainer(this IHtmlHelper html, Teacher teacher, int? scheduleID = null)
        {
            if (teacher != null)
            {
                StringBuilder code = new StringBuilder();
                code.Append(string.Format("<div class='chat-body clearfix' data-teacher-id='{0}'>", teacher.ID));
                code.Append("<div class='header'>");
                code.Append(string.Format("<strong class='primary-font'>{0} {1} {2}</strong>", teacher.LastName, teacher.FirstName, teacher.MiddleName));
                code.Append(string.Format("<small class='pull-right text-muted glyphicon glyphicon-remove' {0} onclick='server.remove($(this));'></small>", "data-schedule-id='" + scheduleID + "'"));
                code.Append("</div><p>");
                code.Append(TeacherSubject(teacher.Subjects));
                code.Append("</p></div>");

                return new HtmlString(code.ToString());
            }

            return new HtmlString("");
        }

        private static string TeacherSubject(IEnumerable<Subject> subjects)
        {
            string s = default(string);
            foreach (var item in subjects)
            {
                s += item.Name + ",";
            }
            return s;
        }
    }
}
