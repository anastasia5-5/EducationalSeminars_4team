using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationalSeminars_4team_Novikova_Nastya
{
    public class Event
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Time {  get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Participants { get; set; }
    }
}
