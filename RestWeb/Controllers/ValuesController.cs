using LLXCORE;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestWeb.Controllers
{
    public class ValuesController : ApiController
    {
        DataBase.Connection Conn = new DataBase.Connection();
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public int Get(int Num1, int Num2)
        {
            return Num1 * Num2;
        }
        [Route("Anonima")]
        public string PostAnonima([FromBody]string json)
        {
            try
            {
                dynamic j = JsonConvert.DeserializeObject(json);
                string fecha = j.fec;
                string hora = j.hor;
                string nombre = j.nom;
                string correo = j.cor;
                string telefono = j.tel;

                LLXCORE.MySQL.Query.Procedure procedure = new LLXCORE.MySQL.Query.Procedure("insert_primera_cita");
                procedure.ParametersAdd(fecha);
                procedure.ParametersAdd(hora);
                procedure.ParametersAdd(nombre);
                procedure.ParametersAdd(correo);
                procedure.ParametersAdd(telefono);
                LLXCORE.MySQL.Statement statement = new LLXCORE.MySQL.Statement(procedure);
                LLXCORE.MySQL.Result result = Conn.Adapter.Execute(statement);
                object[,] datos = result.Data;

                if (int.Parse(datos[0, 0].ToString()) == 1)
                {
                    return "Cita creada correctamente";
                }
                return "Error inesperado.";
            }
            catch (LLXCORE.Failure ex)
            {
                //string Result = "";
                //foreach (string item in ex.Handle())
                //{
                //    Result += item + "\n";
                //}
                //Result += ex.GetInnerestNative().Message;
                return ex.GetInnerestNative().Message;
            }
            catch
            {
                return "Error inesperado.";
            }

        }

        [Route("Test")]
        public string PostTest([FromBody]string json)
        {
            try
            {
                dynamic j = JsonConvert.DeserializeObject(json);
                string dato1 = j.dato1;
                string dato2 = j.dato2;
                string cadena = "{";
                LLXCORE.MySQL.Query.Procedure procedure = new LLXCORE.MySQL.Query.Procedure("test");
                procedure.ParametersAdd(dato1);
                procedure.ParametersAdd(dato2);
                LLXCORE.MySQL.Statement statement = new LLXCORE.MySQL.Statement(procedure);

                LLXCORE.MySQL.Result result = Conn.Adapter.Execute(statement);

                object[,] datos = result.Data;

                for (int i = 0; i < datos.GetLength(0); i++)
                {
                    for (int k = 0; k < datos.GetLength(1); k++)
                    {
                        cadena += "datorow" + i + "column" + k + ":'" + datos[i, k] + "',";
                    }
                }
                cadena += "}";
                return cadena;

            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
        [Route("horas")]
        public string PostHoras([FromBody] string json)
        {

            try
            {
                dynamic j = JsonConvert.DeserializeObject(json);
                string fecha = j.fecha;
                string token = j.token;
                string horas = string.Empty;
                LLXCORE.MySQL.Query.Procedure procedure = new LLXCORE.MySQL.Query.Procedure("citas_horas");
                procedure.ParametersAdd(fecha);
                procedure.ParametersAdd(token);
                LLXCORE.MySQL.Statement statement = new LLXCORE.MySQL.Statement(procedure);

                LLXCORE.MySQL.Result result = Conn.Adapter.Execute(statement);

                object[,] datos = result.Data;

                for (int i = 0; i < datos.GetLength(0); i++)
                {
                    for (int k = 0; k < datos.GetLength(1); k++)
                    {
                        horas += datos[i, k] + ",";
                    }
                }
                return horas;
            }
            catch (LLXCORE.Failure Failure)
            {

                return Failure.GetInnerestNative().Message;
            }

        }
        // POST api/values
        [Route("login")]
        public string Post([FromBody] string json)
        {
            try
            {
                dynamic j = JsonConvert.DeserializeObject(json);
                string pusr = j.pusr;
                string ppsw = j.ppsw;

                LLXCORE.MySQL.Query.Procedure procedure = new LLXCORE.MySQL.Query.Procedure("login");
                procedure.ParametersAdd(pusr);
                procedure.ParametersAdd(ppsw);
                LLXCORE.MySQL.Statement statement = new LLXCORE.MySQL.Statement(procedure);
                LLXCORE.MySQL.Result result = Conn.Adapter.Execute(statement);

                object[,] datos = result.Data;
                if (datos == null)
                {
                    return string.Empty;
                }
                if (datos.GetLength(0) <= 0)
                {
                    return string.Empty;
                }
                object resultado = datos[0, 0];

                return datos[0, 0].ToString();
            }
            catch (LLXCORE.Failure Failure)
            {
                return Failure.GetInnerestNative().Message;
            }
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
