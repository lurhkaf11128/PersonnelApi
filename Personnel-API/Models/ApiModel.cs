namespace Personnel_API.Model
{
    public class ApiModel
    {

        public class Personnel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
            public string Phone { get; set; }

            public ICollection<Sales> Sales { get; set; } // Navigation Property
        }

        public class Sales
        {
            public int Id { get; set; }
            public int PersonnelId { get; set; }
            public DateTime ReportDate { get; set; }
            public decimal SalesAmount { get; set; }

            public Personnel Personnel { get; set; } // Navigation Property
        }



    }
}
