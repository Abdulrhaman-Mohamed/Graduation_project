
using AutoMapper.Internal;
using Newtonsoft.Json.Linq;
using Repo_Core.Interface;
using Repo_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo_EF.Repo_Method
{
    public class CreatePlan : ICreatePlan
    {
        protected ApplicationDbContext _context { get; set; }

        public CreatePlan(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // Front to Back
        public string saveAll(IEnumerable<Plan> plan, byte flag)
        {
            IQueryable<Plan> Query = _context.Plans;
            bool waitting = flag == 1 ? true : false;
            if (waitting is false)
            {

                var time = plan.FirstOrDefault().dateTime;
                var check = Query.Where(x => x.FlagWatting == false).ToList();
                int count = check.DistinctBy(x => x.Id).Count();
                if (count > 6)
                    return "There are more than 5 Plans doesn't Execute";
                if (count > 0)
                {
                    var nearest = check.MinBy(x => Math.Abs((x.dateTime - time).TotalSeconds));
                    if (Math.Abs((nearest.dateTime - time).TotalMinutes) <= 30)
                        return "Can't create This Plan Because There are Plans whose time near this plan";
                }
            }
            foreach (Plan value in plan)
            {
                value.FlagWatting = waitting;
                value.Id = Query.AsEnumerable().Last().Id + 1;
                // Put Serialize here.
            }

            _context.Plans.AddRange(plan);
            _context.SaveChanges();

            return "Save Successful";
        }

        public byte[] SerializerBody(Plan plan)
        {
            // use Array.Reverse(PlanID, 0 ,PlanID.Length); if you want to use big-endian byte order
            byte[] SerializerBody = new byte[7];
            byte[] PlanID = BitConverter.GetBytes((short)plan.Id);
            Array.Reverse(PlanID, 0, PlanID.Length);
            byte SequnceID = (byte)plan.SequenceNumber;
            byte SubSystemID = (byte)plan.SubSystemId;
            byte CommandID = (byte)plan.CommandId;
            byte Delay = byte.Parse(plan.Delay);
            byte CommandRepeat = byte.Parse(plan.Repeat);

            SerializerBody[0] = PlanID[0];
            SerializerBody[1] = PlanID[1];
            SerializerBody[2] = SequnceID;
            SerializerBody[3] = SubSystemID;
            SerializerBody[4] = CommandID;
            SerializerBody[5] = Delay;
            SerializerBody[6] = CommandRepeat;

            //string SerializeBody = string.Concat(plan.Id.ToString("D2"), plan.SequenceNumber.ToString("D1"), plan.SubSystemId.ToString("D1"), plan.CommandId.ToString("D1"),
            //    plan.Delay, plan.Repeat);
            return SerializerBody;
        }

        public Plan DeSerializerBody(byte[] DeSerializerBody) 
        {
            // use Array.Reverse(PlanID, 0 ,PlanID.Length); if the DeSerializerBody array is in big-endian byte order 
            Plan plan = new Plan();

            byte[] PlanID = new byte[2];
            PlanID[0] = DeSerializerBody[0];
            PlanID[1] = DeSerializerBody[1];
            Array.Reverse(PlanID, 0, PlanID.Length);
            int Id = BitConverter.ToInt32(PlanID, 0);
            // the reset of the code will be completed when you accept to a specific data format
            return plan;
        }

    }
}
