# Funciones

# def nombre_funcion(args):
#   instrucciones

# para invocar nombre_funcion(parametros)


#Ejemplo

print("#### Ejemplo ####")

# Definir funciòn
def muestraNombre():
    print("Jorge1")
    print("Jorge2")
    print("Jorge3")

# Invocar funciòn
muestraNombre()


print("#### Ejemplo 2 ####")

def mostrarTuNombre(nombre, edad):
    print(f"Tu nombre es: {nombre} ")

    if edad >= 18:
        print("Y eres mayor de edad")


#nombre = input("Introduce tu nombre: ")
#edad = int(input("Introduce tu edad: "))
#mostrarTuNombre(nombre, edad)


print("#### Ejemplo 3 ####")

def tabla(numero):
    print(f"Tabla de multiplicar del número {numero} ")
    for contador in range(11):
        operacion = numero * contador
        print(f" {numero}*{contador} = {operacion} ")

    print("\n")

tabla(3)
tabla(7)

# Ejemplo 3.1
print("\n")
print("-----------------------------")

for numero_tabla in range(1,11):
    tabla(numero_tabla)


# EJemplo 4
print("#### Ejemplo 4 ####")

# Parametros Opcionales

def getEmpleado(nombre, rut = None):
    print("EMPLEADO")
    print(f"Nombre: {nombre} ")

    if rut != None:
        print(f"Rut: {rut} ")

getEmpleado("Jorge", "17834471-4")


# EJemplo 5 parametros opcionales y return 

def saludame(nombre):
    saludo = f"Hola, saludos {nombre}"

    return saludo

print(saludame("Jorge"))


# Ejemplo 6
print("\n")
print("#### Ejemplo 6 ####")
def calculadora(numero1, numero2, basicas = False):
    suma = numero1+ numero2
    resta = numero1 - numero2
    multi = numero1 * numero2
    division = numero1 / numero2

    cadena = ""

    if basicas != False:
        cadena += "Suma: " + str(suma)
        cadena += "\n"
        cadena += "Resta: " + str(resta)
        cadena += "\n"
    else:
        cadena += "Multiplicación: " + str(multi)
        cadena += "\n"
        cadena += "Division: " + str(division)

    return cadena

print(calculadora(5,5, False))


# Ejemplo 7

print("#### Ejemplo 7 ####")

print("\n")
def getNombre(nombre):
    texto = f"El nombre es: {nombre}"
    return texto

def getApellidos(apellidos):
    texto = f"Los apellidos son: {apellidos} "
    return texto

def devuelveTodo(nombre, apellidos):
    texto = getNombre(nombre) + "\n" + getApellidos(apellidos)
    return texto

print(devuelveTodo("Jorge", "Villaseca"))


#Ejercicio 8 - Funciones Lambda
print("#### Ejercicio 8 ####")

dime_el_year = lambda year: f"el año es { year * 50 }"

print(dime_el_year(2034))