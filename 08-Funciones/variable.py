"""

Variables globales se pueden usar dentro y fuera de las funciones
Variables locales solo se pueden usar dentro de las funciones y no pueden ser usadas desde afuera, salvo que hagamos un return


"""

frase = "Ni los genios son tan genios, ni los mediocres tan mediocres"

print(frase)

def holaMundo():
    frase = "Hola mundo!!"
    print(frase)


holaMundo();