

namespace Structures
{
    public struct Room
    {
        public int m_ID { get; set; }

        public byte m_Exits { get; set; }
        public byte m_X { get; set; }
        public byte m_Y { get; set; }





        public byte[] m_NearBy { get; set; }

        /// <summary>
        /// First bit is a bool saying wether it is or not,
        /// second bit is if its a item room or not
        /// third bit is if its a boss room or not
        /// TODO: fourth bit is if its a shop or not?
        /// </summary>
        public int m_Special { get; set; }

        public int m_PreSet { get; set; }
        
        public Room(int _id, byte _exits, byte _x, byte _y)
        {
            this.m_ID = _id;
            this.m_Exits = _exits;
            this.m_X = _x;
            this.m_Y = _y;
            this.m_NearBy = new byte[4];
            this.m_PreSet = 0;
            this.m_Special = 0;
        }
        public void pre(int num) 
        {
            m_PreSet = num;
        }

        public void special(int num) 
        {
            this.m_Special = num;
        }
    }
}