using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTransportation
{
    public static class SimulationManager
    {
        private static MainForm form;
        private static Logger logger;

        public static void Initialize(MainForm mainForm)
        {
            form = mainForm;
            logger = new Logger("simulation.log", form);
        }

        public static async Task StartSimulation()
        {
            logger.Log("Симуляция запущена.");

            var bulldozer = new Bulldozer(0, form, logger);
            Task.Run(() => bulldozer.Run());

            for (int i = 0; i < 2; i++)
            {
                var loader = new Loader(i, form, logger);
                Task.Run(() => loader.Run());
            }

            for (int i = 0; i < 4; i++)
            {
                var truck = new Truck(i, form, logger);
                Task.Run(() => truck.Run());
            }
        }
    }
}
