using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Football
{
    class InputThread
    {
        private static InputThread _instance;
        public Task TakeData;

        private InputThread() {
            TakeInfoAboutTeams();
        }

        public static InputThread Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new InputThread();
                }
                return _instance;
            }
        }

        private void TakeInfoAboutTeams()
        {
            FormAllTeams teams = new FormAllTeams();
            TakeData = new Task(() => teams.FillData());
            TakeData.Start();
        }

        public async void Start() {
            await TakeData;

        }
    }
}
