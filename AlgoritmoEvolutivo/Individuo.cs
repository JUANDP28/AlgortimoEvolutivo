using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoritmoEvolutivo {

    class Individuo {
        int [] solucion;
        int calidad;

        public Individuo (int[] solucion) {
            this.solucion = solucion;
        }

        public void SetCalidad (int calidad) {
            this.calidad = calidad;
        }

        public int[] GetSolucion () {
            return this.solucion;
        }

        public int GetValor (int posicion) {
            return this.solucion [posicion];
        }
        public int GetCalidad () {
            return this.calidad;
        }
    }
}
