# Encryption

## What it is:
- A reversible process that transforms data into an unreadable format to protect its confidentiality.
- Requires an encryption key to both encrypt and decrypt data.

## Pros:
- **Data Confidentiality**: Protects sensitive data from unauthorized access.
- **Reversible**: Can be decrypted back to the original form with the correct key.
- **Versatile**: Can be used to secure communication (e.g., emails, files).

## Cons:
- **Key Management**: The encryption key must be securely managed and protected.
- **Performance Overhead**: Encryption can introduce performance overhead, especially with large datasets.
- **Vulnerable to Key Compromise**: If the key is compromised, the encrypted data is no longer secure.

## Best Use Cases:
- **Secure Communication** (e.g., email encryption, SSL/TLS).
- **File Encryption** (e.g., encrypted disk drives, cloud storage).
- **Secure APIs and web services** (e.g., encrypting sensitive data like payment details).

---

# Encryption Algorithms & Options

## 1. Symmetric Encryption (Same key for encryption & decryption):

### AES (Advanced Encryption Standard)
- **Best for**: General-purpose encryption, including file encryption, secure data transmission.
- **Recommended Library**: `System.Security.Cryptography.Aes`
- **Key Sizes**: 128, 192, 256 bits

### DES (Data Encryption Standard)
- **Best for**: Legacy systems, but should not be used for new applications due to weaknesses.
- **Recommended Library**: `System.Security.Cryptography.DESCryptoServiceProvider`
- **Key Size**: 56 bits (not recommended for modern systems)

### 3DES (Triple DES)
- **Best for**: Legacy applications that require stronger encryption than DES but still not recommended for new systems.
- **Recommended Library**: `System.Security.Cryptography.TripleDESCryptoServiceProvider`
- **Key Size**: 168 bits

### RC4 (Rivest Cipher 4)
- **Best for**: Stream cipher (used in some legacy protocols like SSL/TLS), but is considered insecure and should be avoided for modern applications.
- **Recommended Library**: `System.Security.Cryptography.RC4` (deprecated, avoid using)
- **Key Size**: 40-2048 bits

## 2. Asymmetric Encryption (Public & Private keys):

### RSA (Rivest–Shamir–Adleman)
- **Best for**: Secure data transmission, digital signatures, and encryption of small amounts of data (like encryption of keys).
- **Recommended Library**: `System.Security.Cryptography.RSA`
- **Key Sizes**: 2048, 3072, 4096 bits

### ECC (Elliptic Curve Cryptography)
- **Best for**: Strong security with smaller key sizes (e.g., for mobile devices, IoT, or situations where performance is important).
- **Recommended Library**: `System.Security.Cryptography.ECDsa` and `System.Security.Cryptography.ECDiffieHellman`
- **Key Sizes**: 256, 384, 521 bits

## 3. Hybrid (Combines Symmetric and Asymmetric):

### Elliptic Curve Integrated Encryption Scheme (ECIES)
- **Best for**: High-performance, secure key exchange for modern encryption.
- **Recommended Library**: There is no built-in .NET support, but third-party libraries like BouncyCastle can be used.
