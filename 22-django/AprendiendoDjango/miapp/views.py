from django.shortcuts import render, HttpResponse

# Create your views here.
# MVC = Modelo Vista Controlador => Acciones (metodos)
# MVT = Modelo Vista Template  => Acciones (metodos)

def holaMundo(request):
    return HttpResponse(
    """<h1>Hola mundo con Django!! <h1>
    <h3>Soy Jorge Vilaseca</h3>
    """)
