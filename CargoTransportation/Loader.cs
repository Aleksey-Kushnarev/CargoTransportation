using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTransportation
{
    public class Loader
    {
        private int id;
        private MainForm form;
        private Logger logger;
        private Random rnd = new();

        public Loader(int id, MainForm form, Logger logger)
        {
            this.id = id;
            this.form = form;
            this.logger = logger;
        }

        public void Run()
        {
            while (true)
            {
                form.UpdateStatus("loader", id, "работает");
                logger.Log($"Погрузчик {id} загружает самосвал...");
                Thread.Sleep(id == 0 ? 14000 : 12000);

                form.UpdateStatus("loader", id, "отдых");
                logger.Log($"Погрузчик {id} отдыхает...");
                Thread.Sleep(5000);
            }
        }
    }
}
