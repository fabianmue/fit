FROM registry.access.redhat.com/ubi9 AS ubi-micro-build
RUN mkdir -p /mnt/rootfs
RUN dnf install --installroot /mnt/rootfs curl --releasever 9 --setopt install_weak_deps=false --nodocs -y && \
    dnf --installroot /mnt/rootfs clean all && \
    rpm --root /mnt/rootfs -e --nodeps setup

FROM quay.io/keycloak/keycloak:26.1 AS build
ENV KC_HEALTH_ENABLED=true
ENV KC_METRICS_ENABLED=true
ENV KC_DB=postgres
RUN /opt/keycloak/bin/kc.sh build

FROM quay.io/keycloak/keycloak:26.1
COPY --from=ubi-micro-build /mnt/rootfs /
COPY --from=build /opt/keycloak/ /opt/keycloak/
ENV KC_DB=postgres
ENTRYPOINT ["/opt/keycloak/bin/kc.sh"]
