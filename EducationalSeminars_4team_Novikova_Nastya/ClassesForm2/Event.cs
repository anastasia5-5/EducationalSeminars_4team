using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationalSeminars_4team_Novikova_Nastya
{
    public class Event
    {
        /// <summary>
        /// Id события
        /// </summary>
        public int EventId { get; set; }
        /// <summary>
        /// Название события
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Дата события
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Время события
        /// </summary>
        public string Time {  get; set; }
        /// <summary>
        /// Категория события
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// Описание события
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Участники события
        /// </summary>
        public string Participants { get; set; }
    }
}
