using System;
using System.Security.Cryptography.X509Certificates;

namespace QuestForge
{
    public class ZoneManager
    {
        public LinkedList<Zone> zones = new LinkedList<Zone>();
        
        public LinkedList<Zone> AddZone(Zone zone, Zone? prevZone, Zone? nextZone)
        {
            if (prevZone == null && nextZone == null)
            {
                zones.AddFirst(zone);
            }
            else if (prevZone == null)
            {
                zones.AddBefore(zones.Find(nextZone), zone);
            }
            else if (nextZone == null)
            {
                zones.AddAfter(zones.Find(prevZone), zone);
            }
            else
            {
                zones.AddAfter(zones.Find(prevZone), zone);
                zones.Remove(nextZone);
                zones.AddAfter(zones.Find(zone), nextZone);
            }
            return zones;
        }

        public Zone GetZoneByName(string name)
        {
            foreach (var zone in zones)
                if (zone.Name == name)
                    return zone;
            return null;
        }

        public bool CanTravel(Zone from, Zone to)
        {
            // Example: allow travel only to adjacent zones in linked list
            var node = zones.Find(from);
            if (node == null) return true; // If from is null, allow travel to any zone (e.g., starting point)
            return node.Next?.Value == to || node.Previous?.Value == to;
        }
    }
}