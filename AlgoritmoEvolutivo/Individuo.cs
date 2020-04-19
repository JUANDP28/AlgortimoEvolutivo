using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoritmoEvolutivo {

    /// <summary>
    /// Clase que se utiliza en el algoritmo para
    /// almacenar las posibles soluciones
    /// </summary>
    class Individuo {

        /// <summary>
        /// Atributos
        /// </summary>
        int [] solucion;
        int calidad;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="solucion">Arreglo de enteros que
        /// lleva la posible solución</param>
        public Individuo (int[] solucion) {
            this.solucion = solucion;
        }

        /// <summary>
        /// Metodo para asignar la calidad del Individuo
        /// </summary>
        /// <param name="calidad"></param>
        public void SetCalidad (int calidad) {
            this.calidad = calidad;
        }

        /// <summary>
        /// Metodo que Obtiene la solución
        /// </summary>
        /// <returns>areeglo de enteros con la posible
        /// solución</returns>
        public int[] GetSolucion () {
            return this.solucion;
        }

        /// <summary>
        /// Metodo que obtiene el valor de una posición
        /// especifica en la solución
        /// </summary>
        /// <param name="posicion"></param>
        /// <returns></returns>
        public int GetValor (int posicion) {
            return this.solucion [posicion];
        }

        /// <summary>
        /// Metodoq ue obtiene la calidad de la solución
        /// </summary>
        /// <returns></returns>
        public int GetCalidad () {
            return this.calidad;
        }
    }
}
