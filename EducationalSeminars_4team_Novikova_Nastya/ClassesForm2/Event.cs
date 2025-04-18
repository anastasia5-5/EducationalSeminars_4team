using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationalSeminars_4team_Novikova_Nastya
{
    public class Event
    {
        /// <summary>
        /// Id события
        /// </summary>
        [DisplayName("Id")]
        public int EventId { get; set; }
        /// <summary>
        /// Название события
        /// </summary>
        [DisplayName("название")]
        public string Title { get; set; }
        /// <summary>
        /// Дата события
        /// </summary>
        [DisplayName("дата")]
        public DateTime Date { get; set; }
        /// <summary>
        /// Время события
        /// </summary>
        [DisplayName("время")]
        public string Time {  get; set; }
        /// <summary>
        /// Категория события
        /// </summary>
        [DisplayName("категория")]
        public string Category { get; set; }
        /// <summary>
        /// Описание события
        /// </summary>
        [DisplayName("описание")]
        public string Description { get; set; }
        /// <summary>
        /// Участники события
        /// </summary>
        [DisplayName("участники")]
        public string Participants { get; set; }
    }
}
