using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSI_Etap3.Content
{
    /// <summary>
    /// Klasa ktora zawiera elementy kazdej twarzy
    /// potrzebne do uzycia w sieci neuronowej. 
    /// name, index identyfikuje zdjecie.
    /// features sa inputem do sieci neuronowej.
    /// networkIndex bedzie oczekiwanym wynikiem sieci.
    /// </summary>
    [Serializable]
    public class Face
    {
        public string Name { get; set; }
        public string FolderName { get; set; }
        public int ClassIndex { get; set; }
        public int NetworkIndex { get; set; }
        public List<float> Features { get; set; }

        /// <summary>
        /// Pusty konstruktor
        /// </summary>
        public Face()
        {
            this.Name = "unknown";
            this.FolderName = "unknown";
            this.ClassIndex = -1;
            this.NetworkIndex = -1;
            this.Features = new List<float>();
        }

        /// <summary>
        /// Metoda sprawdza czy twarz zostala zainicjowana. 1 jak sie uda
        /// -1 jak sie nie uda
        /// </summary>
        public int ValidateFace()
        {
            if (this.Name != "unknown" && this.ClassIndex != -1 && this.NetworkIndex != -1 && this.FolderName != "unknown" && this.Features.Count > 1)
                return 1;
            else
                return -1;
        }
    }
}
