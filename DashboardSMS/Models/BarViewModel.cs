﻿using System.Collections.Generic;

namespace MonitoramentoDigital.Service.MVC.Models
{
    public class BarViewModel
    {
        public string label { get; set; }

        public string fillColor { get; set; }

        public string strokeColor { get; set; }

        public string pointColor { get; set; }

        public string pointStrokeColor { get; set; }

        public string pointHighlightFill { get; set; }

        public string pointHighlightStroke { get; set; }

        public List<int> data { get; set; }

        public string info { get; set; }
    }
}
