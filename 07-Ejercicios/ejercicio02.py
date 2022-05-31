"""

Ejercicio 2. Escribir un script que nos muestre por pantalla todos los numeros pares del 1 al 120    


"""

contador = 0

for contador in range(1,121):
    mostrar = contador % 2

    if mostrar == 0:
        print(f"{contador} es par")