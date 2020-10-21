import json
import os

print("Welcome to the Tileset Coordinates File Generator.")
print("Copyright (C) Octavio Peñaloza 2020")
print("\nThis program will allow you to generate a .json file with the coordinates for every tile in a tileset.")
print("\nThis program is intended for use on the Cataclysm Engine.")
print("\nhttps://github.com/DGTavo88/Cataclysm-Engine\n\n")

fname = input("Tileset Name (don't add the extension): ") + ".json"
fpath = ""

swidth = int(input("Image Width: "))
sheight = int(input("Image Height: "))

twidth = int(input("Tile Width: "))
theight = int(input("Tile Height: "))
tx = 0
ty = 0

tile = 0

coordinates = []

while ty <= sheight:
    tx = 0
    while tx < swidth:
        cc = [tx, ty]
        coordinates.append(cc)
        tx += twidth
    ty += theight


with open(fname, "w", encoding = "utf-8") as datafile:
    #json.dump(coordinates, datafile, ensure_ascii = False, indent = 4)
    json.dump(coordinates, datafile)
    fpath = os.path.realpath(datafile.name)

input("\nTileset Coordinates saved to: %s\n\nPress enter to end program." %(fpath))
