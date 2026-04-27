using BerryApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;

namespace BerryApp.Biz.Machines
{
    public interface IMachineService
    {
        Machine GetMachineData();
    }
}
