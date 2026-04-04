using System;
using System.Security.Cryptography.X509Certificates;

namespace QuestForge
{
    public class ZoneManager
    {
        public LinkedList<Zone> zones = new LinkedList<Zone>();
        
        public LinkedList<Zone> AddZone(Zone zone, Zone? prevZone, Zone? nextZone) // Add a new zone to the linked list, specifying its position relative to existing zones
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

        public void PopNextEvent(Zone zone)
        {
            if (zones.Contains(zone) && zone.Events.Count > 0)
            {
                zone.Events.Pop();
            }
        }
        public bool PushEvent(Zone zone, GameEvent gameEvent)
        {
            if (!zones.Contains(zone))
                return false;
            zone.Events.Push(gameEvent);
            return true;
        }
        public Zone GetZoneByName(string name) // Retrieve a zone by its name, which can be used for navigation and event triggering
        {
            foreach (var zone in zones)
                if (zone.Name == name)
                    return zone;
            return null;
        }
        public bool CanTravel(Zone from, Zone to) // Implement logic to determine if the player can travel from one zone to another, based on the structure of the linked list and any additional rules (e.g., difficulty restrictions)
        {
            // Example: allow travel only to adjacent zones in linked list
            var node = zones.Find(from);
            if (node == null) return true; // If from is null, allow travel to any zone (e.g., starting point)
            return node.Next?.Value == to || node.Previous?.Value == to;
        }
        public string DisplayZones() // Method to display available zones and their difficulties, which can be used in the game's UI
        {
            string result = "Available Zones:\n";
            foreach (var zone in zones)
            {
                result += $"- {zone.Name} (Difficulty: {zone.Difficulty})\n";
            }
            return result;
        }
    }
}