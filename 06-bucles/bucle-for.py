"""

#FOR

for variable in elemento_iterable (lista, rango, etc.)
    bloque de instrucciones

"""

contador = 0
resultado = 0

for contador in range(0,9):
    print("Voy por el " + str(contador))

    resultado = resultado + contador

print(f"el resultado es: {resultado}")


#Ejemplo tablas de multiplicar

print("\n Ejemplo")

numero_usuario = int(input("De qué numero quieres la tabla?: "))

if numero_usuario < 1:
    numero_usuario = 1

print(f"#### Tabla de multiplicar del número {numero_usuario} ####")

for numero_tabla in range(0,11):
    if numero_usuario == 45:
        print("no se pueden mostrar numeros prohibidos")
        break

    print(f" {numero_usuario} x {numero_tabla} = {numero_usuario * numero_tabla} ")
else:
    print("Tabla finalizada.")