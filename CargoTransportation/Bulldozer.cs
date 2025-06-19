using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTransportation
{
    public class Bulldozer
    {
        private int id;
        private MainForm form;
        private Logger logger;
        private Random rnd = new();

        public Bulldozer(int id, MainForm form, Logger logger)
        {
            this.id = id;
            this.form = form;
            this.logger = logger;
        }

        public void Run()
        {
            while (true)
            {
                form.UpdateStatus("bulldozer", id, "работает");
                logger.Log("Бульдозер сгребает песок...");
                Thread.Sleep(rnd.Next(3000, 6000));

                form.UpdateStatus("bulldozer", id, "отдых");
                logger.Log("Бульдозер ждёт...");
                Thread.Sleep(3000);
            }
        }
    }

}
