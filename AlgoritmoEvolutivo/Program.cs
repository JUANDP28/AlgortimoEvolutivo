using System;
using System.Collections.Generic;

namespace AlgoritmoEvolutivo {
    class Program {

        List<Individuo> poblacion = new List<Individuo>();
        List<Individuo> padres = new List<Individuo>();
        List<Individuo> hijos = new List<Individuo>();
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
        /// Metodo que obtiene los 5 padres aleatorios y
        /// y selecciona los 2 mejores
        /// </summary>
        public void SeleccionarPadres () {

            for (int i = 0; i < 5; i++) {
                padres.Add(ObtenerIndividuo(NumeroAleatorioPoblacion()));
            }

            /*foreach (Individuo elemento in padres) {
                Console.WriteLine("Calidad: " + elemento.GetCalidad());
            }*/

            while (padres.Count != 2) {

                int iterador = 1;
                int posición = 0;
                int peorCalidad = 0;

                foreach (Individuo elemento in padres) {

                    if (iterador == 1) {

                        posición = iterador;
                        peorCalidad = elemento.GetCalidad();

                    } else {

                        if (elemento.GetCalidad() > peorCalidad) {

                            posición = iterador;
                            peorCalidad = elemento.GetCalidad();
                        }
                    }

                    iterador++;
                }

                padres.Remove(ObtenerIndividuoPadres(posición));
            }
        }

        /// <summary>
        /// Metodo que nos da un número aleatorio entre 1 y 101
        /// </summary>
        /// <returns>número entre 1 y 101</returns>
        public int NumeroAleatorioPoblacion () {
            Random random = new Random();
            return random.Next(1, 101);
        }

        /// <summary>
        /// Metodo que devuelve un Individuo segun su posición
        /// </summary>
        /// <param name="posicion">variable de tipo int con la
        /// posición del individuo deseado</param>
        /// <returns>Individuo localizado en esa posición</returns>
        public Individuo ObtenerIndividuo (int posicion) {

            int iterador = 1;

            foreach (Individuo elemento in poblacion) {

                if (iterador == posicion) {

                    return elemento;

                } else {

                    iterador++;
                }

            }
            return null;
        }

        /// <summary>
        /// Metodo que Obtiene un Individuo de la lista padres
        /// </summary>
        /// <returns>varible de tipo Individuo que se solicita</returns>
        public Individuo ObtenerIndividuoPadres (int posicion) {

            int iterador = 1;

            foreach (Individuo elemento in padres) {

                if (iterador == posicion) {

                    return elemento;
                }

                iterador++;
            }

            return null;
        }

        /// <summary>
        /// Metodo que cruza a los 2 mejores padres
        /// </summary>
        public void CruzaPadres () {

            int lugarCorte = NumeroAleatorioCruza();
            int [] solucion1 = new int [8];

            for (int i = 0; i < lugarCorte; i++) {

                solucion1 [i] = ObtenerIndividuoPadres(1).GetSolucion() [i];
            }
            for (int i = lugarCorte; i < ObtenerIndividuo(2).GetSolucion().Length; i++) {

                solucion1 [i] = ObtenerIndividuoPadres(2).GetSolucion() [i];
            }

            if (ProbabilidadMutacion()) {

                solucion1 [NumeroAleatorio()] = NumeroAleatorio();
            }

            int [] solucion2 = new int [8];

            for (int i = 0; i < lugarCorte; i++) {

                solucion2 [i] = ObtenerIndividuoPadres(2).GetSolucion() [i];
            }
            for (int i = lugarCorte; i < ObtenerIndividuo(1).GetSolucion().Length; i++) {

                solucion2 [i] = ObtenerIndividuoPadres(1).GetSolucion() [i];
            }

            if (ProbabilidadMutacion()) {

                solucion2 [NumeroAleatorio()] = NumeroAleatorio();
            }

            Individuo hijo1 = new Individuo(solucion1);
            Evaluacion(hijo1);
            Individuo hijo2 = new Individuo(solucion2);
            Evaluacion(hijo2);
            hijos.Add(hijo1);
            hijos.Add(hijo2);
            padres.Clear();
        }

        /// <summary>
        /// Metodo para obtener un número aleatorio
        /// </summary>
        /// <returns>Unvalor entre 1 y 7</returns>
        public int NumeroAleatorioCruza () {
            Random random = new Random();
            return random.Next(1,7);
        }

        /// <summary>
        /// Metodo que determina la probabilidad de mutación
        /// </summary>
        /// <returns>valor verdadero o falso</returns>
        public bool ProbabilidadMutacion () {
            Random random = new Random();
            if (random.Next(1, 11) <9) {
                return true;
            } else {
                return false;
            }
        }
        
        /// <summary>
        /// Metodo que remplaza los hijos por los dos
        /// peores que existen en la población
        /// </summary>
        public void Sustitucion () {
            while (poblacion.Count != 98) {

                int iterador = 1;
                int posición = 0;
                int peorCalidad = 0;

                foreach (Individuo elemento in poblacion) {

                    if (iterador == 1) {

                        posición = iterador;
                        peorCalidad = elemento.GetCalidad();

                    } else {

                        if (elemento.GetCalidad() > peorCalidad) {

                            posición = iterador;
                            peorCalidad = elemento.GetCalidad();
                        }
                    }

                    iterador++;
                }

               poblacion.Remove(ObtenerIndividuo(posición));
            }

            foreach (Individuo elemento in hijos) {
                poblacion.Add(elemento);
            }

            hijos.Clear();
        }

        /// <summary>
        /// Metodoto que evalua si la solución existe en la población
        /// </summary>
        /// <returns></returns>
        public bool Solucion () {
            foreach (Individuo elemento in poblacion) {

                if (elemento.GetCalidad() == 0) {
                    Console.WriteLine("");
                    Console.WriteLine("=============== SOLUCIÓN ===============");
                    Console.WriteLine("");
                    for (int i = 0; i < elemento.GetSolucion().Length; i++) {
                        Console.WriteLine("( " + (i+1) + " , " + (elemento.GetSolucion()[i]+1) + " )");
                    }
                    Console.WriteLine("");
                    Console.WriteLine("========================================");
                    Console.WriteLine("");
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Metodo que inicializa el algoritmo
        /// </summary>
        /// <param name="args"></param>
        static void Main (string [] args) {
            Console.WriteLine("");
            Console.WriteLine("=============== SE INICIO EL ALGORITMO EVOLUTIVO ===============");
            Console.WriteLine("");

            Program menu = new Program();
            menu.CrearPoblación();
            menu.EvaluarPoblacion();

            bool bandera = false;

            while (bandera != true) {

                menu.SeleccionarPadres();
                menu.CruzaPadres();
                menu.Sustitucion();

                if (menu.numeroEvaluaciones > 10000 || menu.Solucion() == true) {

                    bandera = true;
                }
            }

            Console.WriteLine("NUMERO DE EVALUCIONES: " + menu.numeroEvaluaciones);
        }
    }
}
