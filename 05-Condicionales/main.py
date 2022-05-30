"""

if condicion:
    instrucciones
else:
    otras instrucciones

"""

#Ejemplo 1
print("###### Ejemplo 1 #####")

color = "verde"
#color = input("Adivina cual es mi color favorito:")


if color == "rojo":
    print("El color es rojo")
else:
    print("color incorrecto")


print("##### Ejemplo 3 #####")

nombre = "Jorge Villaseca"
ciudad = "Santiago"
continente = "America"
edad = 31
mayoria_edad = 18

if edad >= mayoria_edad:
    print(f" {nombre} es mayor de edad!")

    if continente != "Europa":
        print("El usurio no es de Europeo")
    else:
        print(f"Es europeo y de {ciudad} ")

else:
    print(f"{nombre} NO es mayor de edad ")


print("##### Ejemplo 4 - elif #####")

#dia = int(input("Introduce el dia de la semana"))
dia = 3


# elif es simil de else if
if dia == 1:
    print("es lunes")
elif dia == 2:
    print("es martes")
elif dia == 3:
    print("es miercoles")
elif dia == 4:
    print("es jueves")
elif dia == 5:
    print("es viernes")
else:
    print("es fin de semana")


print("##### Ejemplo 5 #####")

edad_minima  = 18
edad_maxima = 65
#edad_oficial = int(input("¿Tienes edad de trabajar? Introduce tu edad"))
edad_oficial = 19

"""
Operadores Lógicos Python
and Y
or O
! negacion
Not no

"""

if edad_oficial >= edad_minima and edad_oficial <= edad_maxima:
    print("Esta en edad de trabajar")
else:
    print("No esta en edad de trabajar")



print("##### Ejemplo 6 #####")

pais = "Alemania"

if pais == "Mexico" or pais == "España" or pais == "Colombia":
    print(f"{pais} es un pais de habla hispana")
else:
    print(f"{pais} no es un pais de habla hispana")


print("##### Ejemplo 7 #####")

pais = "Rusia"

if not (pais == "Mexico" or pais == "España" or pais == "Colombia"):
    print(f"{pais} No es un pais de habla hispana")
else:
    print(f"{pais} es un pais de habla hispana")


print("##### Ejemplo 8 #####")

pais = "Colombia"

if pais != "Mexico" and pais != "España" and pais != "Colombia":
    print(f"{pais} No es un pais de habla hispana")
else:
    print(f"{pais} Si es un pais de habla hispana")