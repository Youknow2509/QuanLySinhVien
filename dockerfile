# Use Oracle Database Enterprise Edition 19.3.0 as the base image
FROM oracle/database:19.3.0-ee

# Set environment variables
ENV ORACLE_SID=ORCLCDB \
    ORACLE_PDB=ORCLCDB1 \
    ORACLE_PWD=vInh_123 \
    ENABLE_TCPS=true

# Expose the necessary ports
EXPOSE 1521 5500 8080

# Create a volume for persistent data storage
VOLUME ["/opt/oracle/oradata"]

# Default command to run the container
CMD ["start"]
