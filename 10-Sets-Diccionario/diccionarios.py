"""
Diccionario:
Un tipo de dato que almacena un conjunto de datos
en formato clave : valor
es parecido a un array asociativo o un objeto json

"""

persona = {
    "nombre": "jorge",
    "apellidos": "Villaseca Riquelme",
    "web": "https://github.com/poje"
}

# print(type(persona))
# print(persona)
# print(persona["apellidos"])

# Lista de diccionarios
contactos = [
    {
        'nombre':'Antonio',
        'email': 'antonio@antonio.com'
    },
    {
        'nombre':'jorge',
        'email': 'jorge@jorge.com'
    },
    {
        'nombre':'matias',
        'email': 'matias@matias.com'
    },

]

print(contactos)
print("\n")
print(contactos[0]["nombre"]) 

print("--------------------------------------------")

for contacto in contactos:
    print(f"Nombre del contacto: {contacto['nombre']} ")
    print(f"Email de contacto {contacto['email']}")
    print("--------------------------------------------")

