# TecVR
TecNM Zamora VR


## Premisa:
Un maestro se queda atrapado de noche en el Tec porque su auto no enciende. Debe buscar piezas por zonas del campus para repararlo, mientras ocurren eventos extraños.

### GamePlay minimo:
Inicio: estacionamiento / auto descompuesto
Objetivo: conseguir 3 piezas
Zonas: edificio principal, servicios escolares, canchas o ingeniería
Amenaza: entidad simple / evento de horror / IA básica
Final: reparar auto y escapar
### Para cubrir:
Inicio y final claros
Objetivo definido
Reto progresivo
Interacciones
Objeto original
Ambientación
Audio
Enemigo o amenaza
UI básica

# Diseño del escenario y reto jugable
## Greybox modular 3D:
Estacionamiento
Edificio principal
Servicios escolares
Canchas
Zona de ingeniería
Subida/terrazas del cerro

## Reto progresivo será:
Pieza 1: fácil, cerca del auto
Pieza 2: zona más oscura
Pieza 3: aparece la amenaza o se activa evento fuerte
Regreso final: tensión máxima

# Integración de mecánicas
## Mecanicas minimas:
Movimiento VR
Recoger piezas
Colocar piezas en el auto
Activar puertas o zonas
Eventos por trigger

# Enemigo y comportamiento
## entidad con estados:
Idle / Oculto
Aparece
Observa al jugador
Se acerca lentamente
Desaparece
Activa Game Over si toca al jugador

## puede ser con:
Waypoints
Triggers
Distancia al jugador
Estados simples en C#

# Objetos interactuables:
## Minimo 2
Piezas del auto
Caja de herramientas / fusible / cable
Auto reparable
Puerta o interruptor
Linterna opcional

## Hacer 3
Pieza coleccionable
Interruptor/fusible
Auto final

# Interfaz y flujo
Piezas: 0/3
Objetivo actual
Pantalla inicial
Pantalla final
Game Over si te alcanza la entidad

# Sonidos y animaciones
## Minimo:
Audio ambiental nocturno
Sonido al recoger pieza
Sonido de susto/evento
Parpadeo de luz
Animación simple de entidad o aparición

# Mecánica original
Sistema de reparación del auto por piezas encontradas en el campus
y Mientras más piezas encuentras, más activa se vuelve la entidad.








