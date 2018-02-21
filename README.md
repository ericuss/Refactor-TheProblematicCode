# Refactor-The Problematic Code



Esto es un ejemplo de código a refactorizar.

 - NO! NO! NO! ¿que es esto? 
 
 ![alt text](https://static1.squarespace.com/static/5735ffd51d07c093e26f869d/573b8b4274e8d6d04983ad7e/573b8ed974e8d6d04983f0aa/1463527748903/?format=500w "No no no")

 
## Contexto

Todo empezo por intentar solventar un bug. Hay una búsqueda de *Appointments*, tiene varios filtros y entre ellos tiene un filtro por fechas. El bug lo que decía es que si pones los filtros de hoy a hoy no mostraba datos. Al ver como estaba hecho... En resumen, tenemos un código que se tiene que refactorizar, solo con verlo se nota que hay cosas que no están de la forma ... más correcta.
 
## El código
 
Tenemos 2 proyectos en la solución:

1. Refactor.CodeSmells.Filtering
* De este proyecto lo que nos interesa es el *Services/AppointmentService.cs*. Hay una funcion que trata de filtrar unos appointments con diferentes propiedades.
2. Refactor.CodeSmells.Filtering.Tests
* Aquí tenemos 2 tests funcionando y uno que peta. Tenemos que ver porqué peta y arreglarlo.


Primero intentamos solventar el bug. Hay un test roto que intenta filtrar los *appointments* de hoy. Después tendremos que identificar los problemas y ver cómo podemos abordarlos.
