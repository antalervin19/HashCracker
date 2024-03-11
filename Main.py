import string
import random
import hashlib
from concurrent.futures import ThreadPoolExecutor
from time import time

originalHash = "688787d8ff144c502c7f5cffaafe2cc588d86079f9de88304c26b0cb99ce91c6"

def Hash(string):
    string_bytes = string.encode('utf-8')
    sha256_hash = hashlib.sha256()
    sha256_hash.update(string_bytes)
    hashed_string = sha256_hash.hexdigest()
    return hashed_string

def generate_random_string(length):
    characters = string.ascii_lowercase + string.ascii_uppercase + string.digits
    random_string = ''.join(random.choice(characters) for _ in range(length))
    return random_string

def check_hash():
    while True:
        Random = generate_random_string(3)
        if Hash(Random) == originalHash:
            print("Original hash is:", Random)
            return

def Main(num_threads=4):
    with ThreadPoolExecutor(max_workers=num_threads) as executor:
        executor.map(lambda _: check_hash(), range(num_threads))

start = time()
Main()
print(f"Finished after {round(time() - start, 2)} seconds")
