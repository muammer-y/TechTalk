# Hashing

## What it is:
- A one-way process that generates a fixed-size string or value (hash) from input data of any size.
- **Irreversible**: Once data is hashed, it cannot be converted back to its original form.

## Pros:
- **Data Integrity**: Ensures that data hasn’t been tampered with (e.g., checking file integrity).
- **Efficient**: Hashing algorithms are fast and don't require large resources.
- **Irreversible**: No need to store original data; only the hash value is needed for verification.

## Cons:
- **Collision Risk**: Two different inputs may produce the same hash value (collision).
- **Irreversible**: Cannot recover the original data from the hash.
- **Not for Confidentiality**: It does not protect data privacy; just ensures integrity.

## Best Use Cases:
- **Password Storage**: Storing hashed passwords for authentication (e.g., bcrypt, SHA-256).
- **Data Integrity Checks**: Verifying the integrity of files or messages (e.g., checksums).
- **Digital Signatures**: Verifying data authenticity and integrity in secure communications.

---

# Hashing Algorithms & Options

## SHA-256 (Secure Hash Algorithm 256-bit)
- **Best for**: Cryptographic applications (password hashing, digital signatures, checksums).
- **Recommended Library**: `System.Security.Cryptography.SHA256`
- **Output Size**: 256 bits

## SHA-3 (Keccak)
- **Best for**: Next-generation hashing for cryptographic applications; more secure than SHA-2.
- **Recommended Library**: `System.Security.Cryptography.SHA3` (available in some .NET Core versions or third-party libraries)
- **Output Size**: 224, 256, 384, 512 bits

## MD5 (Message Digest Algorithm 5)
- **Best for**: Legacy checksum applications but **NOT recommended** for security purposes due to vulnerabilities (collision attacks).
- **Recommended Library**: `System.Security.Cryptography.MD5`
- **Output Size**: 128 bits  
- **Warning**: Avoid for cryptographic purposes!

## SHA-1 (Secure Hash Algorithm 1)
- **Best for**: Legacy applications (e.g., older digital signatures, certificates), but should be avoided for new applications due to vulnerabilities.
- **Recommended Library**: `System.Security.Cryptography.SHA1`
- **Output Size**: 160 bits  
- **Warning**: Avoid for cryptographic purposes!

## bcrypt
- **Best for**: Password hashing, especially for high-security environments.
- **Recommended Library**: Use third-party libraries like `BCrypt.Net` (not built into .NET).
- **Output Size**: Variable, but designed for slow hashing to thwart brute-force attacks.

## PBKDF2 (Password-Based Key Derivation Function 2)
- **Best for**: Password hashing with a customizable number of iterations to increase computational cost.
- **Recommended Library**: `System.Security.Cryptography.Rfc2898DeriveBytes`
- **Output Size**: User-defined  
- **Best Practice**: Use with salt and a large number of iterations.

## Argon2
- **Best for**: Modern password hashing, designed to be memory-hard to resist GPU and ASIC cracking attacks.
- **Recommended Library**: Use third-party libraries like `Konscious.Security.Cryptography.Argon2`
- **Variants**: Argon2d, Argon2i (for different security needs).
