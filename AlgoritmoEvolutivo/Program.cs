using System;
using System.Collections.Generic;

namespace AlgoritmoEvolutivo {
    class Program {

        List<Individuo> poblacion = new List<Individuo>();
        List<Individuo> padres = new List<Individuo>();
        int numeroEvaluaciones = 0;

        /// <summary>
        /// Metodo que crea la población del algoritmo
        /// </summary>
        public void CrearPoblación () {

            for (int i = 0; i < 100; i++) {

                poblacion.Add(CrearIndividuo());
            }

            Console.WriteLine("");
            Console.WriteLine("=============== SE CREO LA POBLACIÓN ===============");
            Console.WriteLine("");
        }

        /// <summary>
        /// Metodo que crea los un individuo con una solución aleatoria
        /// </summary>
        /// <returns>un individuo con solución aleatoria</returns>
        public Individuo CrearIndividuo () {

            int [] solucion = new int [8];

            for (int i = 0; i < solucion.Length; i++) {

                solucion [i] = NumeroAleatorio();
            }

            return new Individuo(solucion);
        }

        /// <summary>
        /// Metodo que retorna un número aleatorio entre 0 y 7
        /// </summary>
        /// <returns>valor de tipo int entre 0 y 7</returns>
        public int NumeroAleatorio () {

            Random random = new Random();
            return random.Next(0,8);
        }

        /// <summary>
        /// Metodo que evalua a toda la población
        /// </summary>
        public void EvaluarPoblacion () {

            foreach (Individuo elemento in poblacion) {

                Evaluacion(elemento);
            }
        }

        /// <summary>
        /// Metodo que evalua a un individuo
        /// </summary>
        /// <param name="individuo">valor de tipo Individuo</param>
        public void Evaluacion (Individuo individuo) {

            int numeroChoques = 0;

            for(int i = 0; i < individuo.GetSolucion().Length; i++) {

                int numero = individuo.GetValor(i);

                for (int j = i+1; j < individuo.GetSolucion().Length; j++) {

                    if (individuo.GetValor(j) == numero) {

                        numeroChoques++;
                        break;
                    }
                }

                int indicador = 1;
                for (int j = i + 1; j < individuo.GetSolucion().Length; j++) {

                    if (individuo.GetValor(j) == (numero + indicador)) {

                        numeroChoques++;
                        break;
                    }

                    indicador++;
                }

                indicador = 1;
                for (int j = i + 1; j < individuo.GetSolucion().Length; j++) {

                    if (individuo.GetValor(j) == (numero - indicador)) {

                        numeroChoques++;
                        break;
                    }

                    indicador++;
                }
            }

            individuo.SetCalidad(numeroChoques);
            numeroEvaluaciones++;
        }

        /// <summary>
        /// Metodo que obtiene los 5 padres para la cruza
        /// </summary>
        public void SeleccionarPadres () {

            int minimoChoques = MinimoDeChoques();
            int iterador = 0;

            while (padres.Count < 5) {

                foreach (Individuo elemento in poblacion) {

                    if (elemento.GetCalidad() == (minimoChoques + iterador)) {

                        if (padres.Count != 5) {
                            padres.Add(elemento);
                        }
                    }
                }
                iterador++;
            }
        }

        /// <summary>
        /// Metodo que calcula el minimo de choques
        /// </summary>
        /// <returns>varible de tipo int con el minimo de choques</returns>
        public int MinimoDeChoques () {

            int minimoChoques = 0;
            int iterador = 1;

            foreach (Individuo elemento in poblacion) {

                if (iterador == 1) {

                    minimoChoques = elemento.GetCalidad();

                } else {

                    if (minimoChoques > elemento.GetCalidad()) {

                        minimoChoques = elemento.GetCalidad();
                    }
                }

                iterador++;
            }
            return minimoChoques;
        }
        


        static void Main (string [] args) {
            Console.WriteLine("");
            Console.WriteLine("=============== SE INICIO EL ALGORITMO EVOLUTIVO ===============");
            Console.WriteLine("");

            Program menu = new Program();
            menu.CrearPoblación();
            menu.EvaluarPoblacion();

            menu.SeleccionarPadres();
        }
    }
}
