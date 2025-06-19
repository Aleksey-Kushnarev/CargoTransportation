using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTransportation
{
    public class Truck
    {
        private int id;
        private MainForm form;
        private Logger logger;
        private Random rnd = new();

        public Truck(int id, MainForm form, Logger logger)
        {
            this.id = id;
            this.form = form;
            this.logger = logger;
        }

        public void Run()
        {
            while (true)
            {
                form.UpdateStatus("truck", id, "работает");
                logger.Log($"Самосвал {id} в пути на разгрузку...");
                form.UpdateStatus("truck", id, "moving");
                Thread.Sleep(10000);

                form.UpdateStatus("truck", id, "отдых");
                logger.Log($"Самосвал {id} разгружается...");
                Thread.Sleep(5000);
            }
        }
    }
}
