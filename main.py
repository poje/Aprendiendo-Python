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

nombre = input("Introduce tu nombre: ")
edad = int(input("Introduce tu edad: "))
mostrarTuNombre(nombre, edad)
