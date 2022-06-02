# Capturar excepciones y manejar errores en codigo
# Susceptible a fallos y errores

"""
try:
    nombre = input("¿Cual es tu nombre?: ")

    if len(nombre) > 1:
        nombre_usuario = f"El nombre es {nombre}"

    print(nombre_usuario)

except:
    print("Ha ocurrido un error, ingrese bien el nombre")
else:
    print("todo ha funcionado correctamente")
finally:
    print("Fin de la iteración")
"""

# Multiples Excepciones
"""
try:
    numero = int(input("Numero para elevarlo al cuadrado: "))
    print("el cuadrado es: " + str(numero*numero))
except TypeError:
    print("debes convertir tus cadenas a enteros en el código")
# except ValueError:
#     print("Introduce un número correcto")
except Exception as e:
    print(type(e))
    print("ha ocurrido un error: ", type(e).__name__)
"""

# Excepciones Personalizadas

try:
    nombre = input("Introduce el nombre: ")
    edad = int(input("Introduce la edad: "))

    if edad < 5 or edad > 110:
        raise ValueError("La edad introducida no es real")
    elif len(nombre) <= 1:
        raise ValueError("El nombre no esta completo")
    else:
        print(f"Bienvenido {nombre}!!")
except ValueError:
    print("Introduce los datos correctamente !")
except Exception as e:
    print("Existe un error ", e)