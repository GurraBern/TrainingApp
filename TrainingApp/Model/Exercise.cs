using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingApp.Model
{
    internal class Exercise
    {
        [PrimaryKey, AutoIncrement]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PrimaryMuscle { get; set; }
        public string SecondaryMuscle { get; set; }
        public string Equipment { get; set; }
    }
}
