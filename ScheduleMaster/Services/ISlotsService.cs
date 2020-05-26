using ScheduleMaster.Models;
using System.Collections.Generic;

namespace ScheduleMaster.Services
{
    public interface ISlotsService
    {
        void AddSlot(int scheduleID, int dayID, int taskID, int startSlot, int slotLength);
        void DeleteSlot(int slotID, int? userID = null);
        List<SlotModel> GetAllSlot(int scheduleID);
        SlotModel GetSlot(int id);
        void UpdateSlot(int slotID, int dayID, int taskID, int startSlot, int slotLength);
        public List<SlotModel> GetAllSlotByDayId(int dayID);

        public SlotTaskModel GetTaskForSlot(int scheduleID, int dayID, int startSlot);

    }
}